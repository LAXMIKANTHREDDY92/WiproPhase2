using CustomerApp1.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace CustomerApp1.Models
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }
}