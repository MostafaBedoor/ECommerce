
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace ECommerce.Models
{
    [Table("Product")]
    public class Product
    {
        public Product()
        {
        }

        public Product(ProductDTO dTO)
        {
            this.Name = dTO.Name;
            this.Description = dTO.Description;
            this.Price = dTO.Price;
            this.Quantity = dTO.Quantity;
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [Column("ProductName")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; } = 0;
    }

    public class ProductDTO
    {

        [Required]
        [Column("ProductName")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName ="decimal(18, 2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; } = 0;
    }

}
