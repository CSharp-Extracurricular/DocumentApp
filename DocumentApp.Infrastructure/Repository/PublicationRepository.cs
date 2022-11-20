using DocumentApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace DocumentApp.Infrastructure
{
    public class PublicationRepository
    {
        private readonly Context _context;

        public Context UnitOfWork => _context;

        public PublicationRepository(Context context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<List<Publication>?> GetAllAsync() => await _context.Publications
            .Include(s => s.Authors)
            .Include(s => s.CitationIndices)
            .Include(s => s.Conference)
            .OrderBy(p => p.Title)
            .ToListAsync();

        public async Task<Publication?> GetByIdAsync(Guid id) => await _context.Publications
            .Where(a => a.Id == id)
            .Include(s => s.Authors)
            .Include(s => s.CitationIndices)
            .Include(s => s.Conference)
            .FirstOrDefaultAsync();

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

        public async Task<int> UpdateAsync(Publication publication)
        {
            await EnsureEntryInContext(publication.Authors);
            await EnsureEntryInContext(publication.CitationIndices);

            if (publication.Conference != null)
            {
                await EnsureEntryInContext(publication.Conference);
            }

            _context.Update(publication);

            return await _context.SaveChangesAsync();
        }

        private async Task EnsureEntryInContext<T>(T entry) where T : class, IIdentifiableT
        {
            T? existingEntry = await _context.FindAsync<T>(entry.Id);

            if (existingEntry == null)
            {
                await _context.AddAsync(entry);
            }
            else
            {
                _context.Update(entry);
            }
        }

        private async Task EnsureEntryInContext<T>(List<T> entry) where T : class, IIdentifiableT
        {
            foreach (T element in entry)
            {
                await EnsureEntryInContext(element);
            }
        }
    }
}
