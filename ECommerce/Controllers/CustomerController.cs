using ECommerce.Contexts;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _config;
        DbSet<Customer> _customers;
        ECommerceDBContext _context;
        public CustomerController(IConfiguration config)
        {
            _config = config;
            _context = new ECommerceDBContext(_config);
            _customers = _context.Customers;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _customers;
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _customers.FirstOrDefault(c => c.ID.Equals(id));
        }

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] CustomerDTO newCustomerDTO)
        {
            Customer newCustomer = new Customer(newCustomerDTO);
            _customers.Add(newCustomer);
            _context.SaveChanges();
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CustomerDTO editCustomerDTO)
        {
            Customer editCustomer = new Customer(editCustomerDTO);
            editCustomer.ID = id;
            _customers.Update(editCustomer);
            _context.SaveChanges();
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Customer deleteCustomer = _customers.FirstOrDefault(c => c.ID.Equals(id));

            if (deleteCustomer != null)
            {
                _customers.Remove(deleteCustomer);
                _context.SaveChanges();
            }
        }
    }
}
