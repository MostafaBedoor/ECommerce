using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ECommerce.Models
{
    [Table("OrderItem")]
    public class OrderItem
    {
        public OrderItem()
        {
        }

        public OrderItem(OrderItemDTO dTO)
        {
            ProductId = dTO.ProductId;
            Quantity = dTO.Quantity;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("FK_OrderItems_Order")]
        public int OrderId { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int ProductId { get; set; }

        [Required]
        public int Quantity {get; set;}
    }

    public class OrderItemDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
