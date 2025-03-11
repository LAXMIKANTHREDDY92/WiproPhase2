using Microsoft.EntityFrameworkCore;
using BatchAPI.Models;

namespace BatchAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Batch> Batches { get; set; }
    }
}
