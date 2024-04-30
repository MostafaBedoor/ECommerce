
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Customer")]
    public class Customer
    {
        public Customer() { }
        public Customer(CustomerDTO dTO)
        {
            Name = dTO.Name;
            Email = dTO.Email;
            Address = dTO.Address;
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [Column("CustomerName")]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Column("CustomerAddress")]
        public string Address { get; set; } = string.Empty;
    }

    public class CustomerDTO
    {
        [Required]
        [Column("CustomerName")]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Column("CustomerAddress")]
        public string Address { get; set; } = string.Empty;
    }
}
