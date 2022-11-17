using DocumentApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DocumentApp.Tests
{
    public class TestHelper
    {
        private readonly Context _context;
        private readonly PublicationRepository _testRepository;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "DocumentApp");

            var dbContextOptions = builder.Options;
            _context = new Context(dbContextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Database.AutoSavepointsEnabled = false;
            _context.Database.AutoTransactionsEnabled = false;
            _context.SaveChanges();

            _testRepository = new(_context);
        }

        public PublicationRepository TestRepository => _testRepository;
    }
}
