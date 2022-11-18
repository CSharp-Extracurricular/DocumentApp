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
            await EnsureEntryCollectionInContext(publication.Authors);
            await EnsureEntryCollectionInContext(publication.CitationIndices);

            if (publication.Conference != null)
            {
                Conference? tempConference = await _context.FindAsync<Conference>(publication.Conference.Id);

                if (tempConference == null)
                {
                    _context.Add(publication.Conference);
                }
            }

            _context.Update(publication);

            return await _context.SaveChangesAsync();
        }

        private async Task EnsureEntryCollectionInContext<T>(List<T> collection) where T : class, IIdentifiableT
        {
            foreach (T entry in collection)
            {
                T? existingEntry = await _context.FindAsync<T>(entry.Id);

                if (existingEntry == null)
                {
                    _context.Add(entry);
                }
            }
        }
    }
}
