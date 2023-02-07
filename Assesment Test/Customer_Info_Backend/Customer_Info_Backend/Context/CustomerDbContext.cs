using Customer_Info_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer_Info_Backend.Context
{
    public class CustomerDbContext:DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext>options):base(options) { }
        public DbSet<Countries> Country { get; set; }
        public DbSet<Customers> Customer { get; set; }
        public DbSet<CustomerAddresses> CustomerAddress { get; set; } 
    }

}
