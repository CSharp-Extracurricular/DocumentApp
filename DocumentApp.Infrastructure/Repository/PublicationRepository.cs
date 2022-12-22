using DocumentApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace DocumentApp.Infrastructure
{
    public class PublicationRepository
    {
        private readonly Context _context;
        public Context UnitOfWork => _context;

        public PublicationRepository(Context context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<List<Publication>> GetAllAsync() => await GetAllAsIQueryable().ToListAsync();

        public async Task<List<Publication>> GetAllAsync(PublicationQuery filtrationQuery)
        {
            IQueryable<Publication> result = GetAllAsIQueryable();

            if (filtrationQuery.StartYear != null)
            {
                result = result.Where(a => a.PublishingYear >= filtrationQuery.StartYear);
            }

            if (filtrationQuery.EndYear != null)
            {
                result = result.Where(a => a.PublishingYear <= filtrationQuery.EndYear);
            }

            if (filtrationQuery.PublicationType != null)
            {
                result = result.Where(a => a.PublicationType == filtrationQuery.PublicationType);
            }

            return await result.ToListAsync();
        }

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
            Publication? existingEntry = await GetByIdAsync(publication.Id);

            if (existingEntry != null)
            {
                UpdateContextEntryValues(publication, existingEntry);
                EnsureEntryInActualState(publication.Authors, existingEntry.Authors);
                EnsureEntryInActualState(publication.CitationIndices, existingEntry.CitationIndices);
                EnsureEntryInActualState(publication.Conference, existingEntry.Conference);
            }

            return await _context.SaveChangesAsync();
        }

        private IQueryable<Publication> GetAllAsIQueryable() => _context.Publications
            .Include(s => s.Authors)
            .Include(s => s.CitationIndices)
            .Include(s => s.Conference)
            .OrderBy(p => p.Title);

        private void EnsureEntryInActualState<T>(ICollection<T> actualEntryContainer, ICollection<T> existingEntryContainer) where T : class, IIdentifiableT
        {
            EnsureEntryInContext(actualEntryContainer, existingEntryContainer);
            EnsureNoOutdatedEntriesInContext(actualEntryContainer, existingEntryContainer);
        }

        private void EnsureEntryInActualState<T>(T? actualEntry, T? existingEntry) where T : class, IIdentifiableT
        {
            if (actualEntry != null)
            {
                EnsureEntryInContext(actualEntry, ref existingEntry);
            }
            else if (existingEntry != null)
            {
                RemoveEntryFromContext(existingEntry);
            }
        }

        private void EnsureEntryInContext<T>(ICollection<T> actualEntryContainer, ICollection<T> existingEntryContainer) where T : class, IIdentifiableT
        {
            foreach (T element in actualEntryContainer)
            {
                EnsureEntryInContext(element, existingEntryContainer);
            }
        }

        private void EnsureNoOutdatedEntriesInContext<T>(ICollection<T> actualEntryContainer, ICollection<T> existingEntryContainer) where T : class, IIdentifiableT
        {
            foreach (T existingEntry in existingEntryContainer)
            {
                RemoveEntryFromContextIfOutdated(existingEntry, actualEntryContainer);
            }
        }

        private void EnsureEntryInContext<T>(T actualEntry, ref T? existingEntryRoot) where T : class
        {
            if (existingEntryRoot == null)
            {
                existingEntryRoot = actualEntry;
            }
            else
            {
                UpdateContextEntryValues(actualEntry, existingEntryRoot);
            }
        }

        private void EnsureEntryInContext<T>(T entry, ICollection<T> existingEntryContainer) where T : class, IIdentifiableT
        {
            T? existingEntry = existingEntryContainer.FirstOrDefault(p => p.Id == entry.Id);

            if (existingEntry == null)
            {
                existingEntryContainer.Add(entry);
            }
            else
            {
                UpdateContextEntryValues(entry, existingEntry);
            }
        }

        private void RemoveEntryFromContextIfOutdated<T>(T existingEntry, IEnumerable<T> actualEntryContainer) where T : class, IIdentifiableT
        {
            if (actualEntryContainer.All(a => a.Id != existingEntry.Id))
            {
                RemoveEntryFromContext(existingEntry);
            }
        }

        private void RemoveEntryFromContext<T>(T existingEntry) where T : class => _context.Remove(existingEntry);

        private void UpdateContextEntryValues<T>(T actual, T existing) where T : class => _context.Entry(existing).CurrentValues.SetValues(actual);
    }
}
