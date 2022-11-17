using DocumentApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace DocumentApp.Infrastructure
{
    public class PublicationRepository
    {
        private readonly Context _context;

        public void CloseConnection() => _context.Dispose();

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
            Publication tempPublication = await GetByIdAsync(publication.Id)
                ?? throw new NullReferenceException("Publication not found");

            _context.Entry(tempPublication).CurrentValues.SetValues(publication);

            foreach (Author author in publication.Authors)
            {
                Author? tempAuthor = tempPublication.Authors.FirstOrDefault(p => p.Id == author.Id);

                if (tempAuthor == null)
                {
                    tempPublication.Authors.Add(author);    
                }
                else
                {
                    _context.Entry(tempAuthor).CurrentValues.SetValues(author);
                }
            }

            //foreach (CitationIndex index in publication.CitationIndices)
            //{
            //    CitationIndex? tempIndex = tempPublication.CitationIndices.FirstOrDefault(p => p.Id == index.Id);

            //    if (tempIndex == null)
            //    {
            //        tempPublication.CitationIndices.Add(index);
            //    }
            //    else
            //    {
            //        _context.Entry(tempIndex).CurrentValues.SetValues(index);
            //    }
            //}

            int changedEntriesCount = await _context.SaveChangesAsync();

            return changedEntriesCount;
        }
    }
}
