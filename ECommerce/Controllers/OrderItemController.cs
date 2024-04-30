using ECommerce.Contexts;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IConfiguration _config;
        DbSet<OrderItem> _OrderItems;
        ECommerceDBContext _context;
        ProductController _productController;
        public OrderItemController(IConfiguration config, ProductController productController)
        {
            _config = config;
            _context = new ECommerceDBContext(_config);
            _OrderItems = _context.OrderItems;
            _productController = productController;
        }

        // GET: api/<OrderItemController>
        [HttpGet]
        public IEnumerable<OrderItem> Get()
        {
            return _OrderItems;
        }

        // GET api/<OrderItemController>/5
        [HttpGet("{id}")]
        public OrderItem Get(int id)
        {
            return _OrderItems.FirstOrDefault(oi => oi.Id.Equals(id));
        }

        // GET api/<OrderItemController>/5
        [HttpGet("{orderId}")]
        public Tuple<IEnumerable<OrderItem>, decimal> GetByOrderId(int orderId)
        {
            List<OrderItem> orderItems = _OrderItems.Where(oi => oi.OrderId.Equals(orderId)).ToList();

            //It can be modified to be done in one transaction
            orderItems.ForEach(oi => oi.Product = _productController.Get(oi.ProductId));
            decimal totalAmount = _OrderItems.Where(oi => oi.OrderId.Equals(orderId)).Sum(oi => (oi.Quantity * oi.Product.Price));
            return new Tuple<IEnumerable<OrderItem>,decimal>(orderItems, totalAmount);
        }

        // POST api/<OrderItemController>
        [HttpPost]
        public void Post([FromBody] OrderItemDTO newOrderItemDTO)
        {
            OrderItem newOrderItem = new OrderItem(newOrderItemDTO);
            _OrderItems.Add(newOrderItem);
            _context.SaveChanges();
        }

        // PUT api/<OrderItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] OrderItemDTO editOrderItemDTO)
        {
            OrderItem editOrderItem = new OrderItem(editOrderItemDTO);
            editOrderItem.Id = id;
            _OrderItems.Update(editOrderItem);
            _context.SaveChanges();
        }

        // DELETE api/<OrderItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            OrderItem deleteOrderItem = _OrderItems.FirstOrDefault(oi => oi.Id.Equals(id));

            if (deleteOrderItem != null)
            {
                _OrderItems.Remove(deleteOrderItem);
                _context.SaveChanges();
            }
        }

        internal void DeleteByOrderId(int id)
        {
            IEnumerable<OrderItem> deleteOrderItems = _OrderItems.Where(oi => oi.OrderId.Equals(id));

            if (deleteOrderItems != null)
            {
                _OrderItems.RemoveRange(deleteOrderItems);
                _context.SaveChanges();
            }
        }
    }
}
