using DocumentApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace DocumentApp.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publication>()
                .HasOne(p => p.Conference)
                .WithOne(b => b.Publication)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Publication> Publications { get; set; } = null!;
    }
}