using Microsoft.EntityFrameworkCore;

namespace DocumentApp.Tests
{
    public class TestHelper
    {
        private readonly Context _context;

        public PublicationRepository TestRepository => new(_context);

        public TestHelper()
        {
            DbContextOptionsBuilder<Context> builder = new();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=Test; Integrated Security=True; Trusted_Connection=True");

            DbContextOptions<Context> dbContextOptions = builder.Options;
            _context = new Context(dbContextOptions);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.ChangeTracker.Clear();
            _context.SaveChanges();
        }
    }
}