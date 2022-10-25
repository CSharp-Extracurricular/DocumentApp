using DocumentApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace AssociationsProcessing.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public DbSet<Publication> Publications { get; set; } = null!;
    }
}