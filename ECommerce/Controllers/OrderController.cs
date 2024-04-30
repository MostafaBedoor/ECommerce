using ECommerce.Contexts;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _config;
        DbSet<Order> _Orders;
        ECommerceDBContext _context;
        CustomerController _customerController;
        OrderItemController _orderItemController;
        public OrderController(IConfiguration config, CustomerController customerController, OrderItemController orderItemController)
        {
            _config = config;
            _context = new ECommerceDBContext(_config);
            _Orders = _context.Orders;
            _customerController = customerController;
            _orderItemController = orderItemController;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<Order> GetAll(int? limit)
        {
            IEnumerable<Order> orders = new List<Order>();

            if (limit.HasValue)
            {
                orders = _Orders.Take(limit.Value);
            }
            else
            {
                orders = _Orders;
            }
            if (orders != null)
            {
                orders.ToList().ForEach(o =>
                {
                    o.Customer = _customerController.Get(o.CustomerId);
                    Tuple<IEnumerable<OrderItem>, decimal> items = _orderItemController.GetByOrderId(o.ID);
                    o.items = items.Item1;
                    o.TotalAmount = items.Item2;
                });
                return orders;
            }
            return null;
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            Order order = _Orders.FirstOrDefault(o => o.ID.Equals(id));
            if (order != null)
            {
                order.Customer = _customerController.Get(order.CustomerId);
                Tuple<IEnumerable<OrderItem>, decimal> items = _orderItemController.GetByOrderId(id);
                order.items = items.Item1;
                order.TotalAmount = items.Item2;
                return order;
            }
            return null;
        }

        [HttpGet("GetByCustomer/{customerId}")]
        public IEnumerable<Order> GetByCustomer(int customerId)
        {
            IEnumerable<Order> orders = _Orders.Where(o => o.CustomerId.Equals(customerId));
            if (orders != null)
            {
                orders.ToList().ForEach(o =>
                {
                    o.Customer = _customerController.Get(o.CustomerId);
                    Tuple<IEnumerable<OrderItem>, decimal> items = _orderItemController.GetByOrderId(o.ID);
                    o.items = items.Item1;
                    o.TotalAmount = items.Item2;
                });
                return orders;
            }
            return null;
        }

        // POST api/<OrderController>
        [HttpPost]
        public ActionResult Post([FromBody] OrderDTO newOrderDTO)
        {
            try
            {
                Order newOrder = new Order(newOrderDTO);
                _Orders.Add(newOrder);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return BadRequest(ex.InnerException.Message);
                else
                    return BadRequest(ex.Message);
            }
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] OrderDTO editOrderDTO)
        {
            Order editOrder = new Order(editOrderDTO);
            editOrder.ID = id;
            _Orders.Update(editOrder);

            _context.SaveChanges();
        }

        [HttpPut("updateStatus/{orderId}/{newStatus}")]
        public void updateStatus(int orderId, OrderStatus newStatus)
        {
            Order editOrder = Get(orderId);
            editOrder.OrderStatus = newStatus;
            _Orders.Update(editOrder);

            _context.SaveChanges();
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Order deleteOrder = _Orders.FirstOrDefault(c => c.ID.Equals(id));

            if (deleteOrder != null)
            {
                _Orders.Remove(deleteOrder);
                _orderItemController.DeleteByOrderId(id);
                _context.SaveChanges();
            }
        }
    }
}
