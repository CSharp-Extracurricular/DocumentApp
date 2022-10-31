using DocumentApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace DocumentApp.Infrastructure
{
    public class PublicationRepository
    {
        private readonly Context _context;
        public Context UnitOfWork => _context;

        public PublicationRepository(Context context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<List<Publication>> GetAllAsync() => await _context.Publications.OrderBy(p => p.Title).ToListAsync();

        public async Task<Publication?> GetByIdAsync(Guid id)
        {
            return await _context.Publications
                .Where(a => a.Id == id)
                .Include(s => s.Authors)
                .Include(b => b.Conference)
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddAsync(Publication publication)
        {
            _context.Publications.Add(publication);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteByIdAsync(Guid id)
        {
            _context.Publications.Remove(await GetByIdAsync(id) ?? null!);
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Publication publication)
        {
            Publication? existPublication = await _context.Publications.FindAsync(publication.Id) ?? null!;
            _context.Entry(existPublication).CurrentValues.SetValues(publication);
            await _context.SaveChangesAsync();
        }
    }
}
