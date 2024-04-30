using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ECommerce.Contexts
{
    public class ECommerceDBContext:DbContext
    {
        private readonly IConfiguration _configuration;

        public ECommerceDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQL"));
        }
    }
}
