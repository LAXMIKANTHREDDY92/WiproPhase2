using Microsoft.EntityFrameworkCore;
using RecipeManagementAPI.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RecipeManagementAPI.Data // Same here
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }

        // Fluent API (Optional if you want to configure relationships)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: enforce unique username
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // If Recipe has a navigation property to Category
            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Category)
                .WithMany()  // Or .WithMany(c => c.Recipes) if you have a collection in Category
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
