using Microsoft.EntityFrameworkCore;
using DocumentApp.Infrastructure;

namespace TestPublication
{
    public class TestHelper
    {
        private readonly Context _context;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "DocumentApp");

            var dbContextOptions = builder.Options;
            _context = new Context(dbContextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public PublicationRepository publicationRepository
        {
            get
            {
                return new PublicationRepository(_context);
            }
        }
    }
}
