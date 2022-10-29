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

            // Вариант для тестирования на изолированной копии базы данных:
            // builder.UseInMemoryDatabase(databaseName: "DocumentApp");

            // Вариант для тестирования на реальной базе данных:
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DocumentApp;Integrated Security=true");

            var dbContextOptions = builder.Options;
            _context = new Context(dbContextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public PublicationRepository TestRepository => new(_context);
    }
}
