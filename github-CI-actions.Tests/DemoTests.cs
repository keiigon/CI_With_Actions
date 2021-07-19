using github_CI_actions.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace github_CI_actions.Tests
{
    public class DemoTests
    {
        [Fact]
        public void Test1()
        {
            Assert.True(1 == 1);
        }

        [Fact]
        public async Task CustomerIntegrationTest()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            var context = new CustomerContext(optionsBuilder.Options);

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            var sut = new CustomersController(context);

            await sut.Add(new Customer { CustomerName = "FooBar" });

            var result = (await sut.GetAll()).ToList();

            Assert.Single(result);
            Assert.Equal("FooBar", result[0].CustomerName);
        }
    }
}
