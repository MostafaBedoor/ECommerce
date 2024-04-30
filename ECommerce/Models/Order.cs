
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace ECommerce.Models
{
    [Table("Order")]
    public class Order
    {
        public Order()
        {
        }

        public Order(OrderDTO dTO)
        {
            CustomerId = dTO.CustomerId;
            OrderStatus = dTO.OrderStatus;

            items = dTO.Items.Select<OrderItemDTO, OrderItem>(o => new OrderItem(o)).ToList();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [NotMapped]
        public decimal TotalAmount { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public IEnumerable<OrderItem> items { get; set; }
    }

    public class OrderDTO
    {
        [Required]
        public int CustomerId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        [ForeignKey("FK_OrderItems_Order")]
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }

    public enum OrderStatus
    {
        pending = 1,
        shipped = 2,
        delivered = 3
    }
}
