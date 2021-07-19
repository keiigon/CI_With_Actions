using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace github_CI_actions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomersController(CustomerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAll() => await _context.Customers.ToArrayAsync();

        [HttpPost]
        public async Task<Customer> Add([FromBody] Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}
