using ECommerce.Contexts;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _config;
        DbSet<Product> _Products;
        ECommerceDBContext _context;
        public ProductController(IConfiguration config)
        {
            _config = config;
            _context = new ECommerceDBContext(_config);
            _Products = _context.Products;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _Products;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _Products.FirstOrDefault(p => p.ID.Equals(id));
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] ProductDTO newProductDTO)
        {
            Product newProduct = new Product(newProductDTO);
            _Products.Add(newProduct);
            _context.SaveChanges();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProductDTO editProductDTO)
        {
            Product editProduct = new Product(editProductDTO);
            editProduct.ID = id;
            _Products.Update(editProduct);
            _context.SaveChanges();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Product deleteProduct = _Products.FirstOrDefault(p => p.ID.Equals(id));

            if (deleteProduct != null)
            {
                _Products.Remove(deleteProduct);
                _context.SaveChanges();
            }
        }
    }
}
