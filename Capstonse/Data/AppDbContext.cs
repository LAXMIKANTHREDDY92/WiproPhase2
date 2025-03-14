using Microsoft.EntityFrameworkCore;
using RecipeManagementAPI.Models;

namespace RecipeManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure UserId is auto-incremented in the DB schema
            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd(); // Ensure auto-increment is enforced

            // ✅ Seed Admin User (With Explicit UserId)
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1, // Must be provided explicitly
                Email = "admin@gmail.com",
                Password = "admin123", // Plain text (Use hashed passwords for production)
                Role = "Admin"
            });
        }
    }
}
