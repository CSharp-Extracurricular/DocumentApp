using DocumentApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;

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
            Publication? existingEntry = await _context.FindAsync<Publication>(publication.Id);

            if (existingEntry != null)
            {
                _context.Entry(existingEntry).CurrentValues.SetValues(publication);

                foreach (var author in publication.Authors)
                {
                    var existingAuthor = existingEntry.Authors.FirstOrDefault(p => p.Id == author.Id);
                    
                    if (existingAuthor == null)
                    {
                        _context.Add(author);
                    }
                    else
                    {
                        _context.Entry(existingAuthor).CurrentValues.SetValues(author);
                    }
                }
                foreach (var existingAuthor in existingEntry.Authors)
                {
                    if (!publication.Authors.Any(a => a.Id == existingAuthor.Id))
                    {
                        _context.Remove(existingAuthor);
                    }
                }

                foreach (var index in publication.CitationIndices)
                {
                    var existingIndex = existingEntry.CitationIndices.FirstOrDefault(p => p.Id == index.Id);
                    
                    if (existingIndex == null)
                    {
                        _context.Add(index);
                    }
                    else
                    {
                        _context.Entry(existingIndex).CurrentValues.SetValues(index);
                    }
                }
                foreach (var existingIndex in existingEntry.CitationIndices)
                {
                    if (!publication.CitationIndices.Any(a => a.Id == existingIndex.Id))
                    {
                        _context.Remove(existingIndex);
                    }
                }

                if (publication.Conference != null)
                {
                    var existingConference = existingEntry.Conference;

                    if (existingConference == null)
                    {
                        _context.Add(existingConference);
                    }
                    else
                    {
                        _context.Entry(existingConference).CurrentValues.SetValues(publication.Conference);
                    }
                }
                else
                {
                    var existingConference = existingEntry.Conference;

                    if (existingConference != null)
                    {
                        _context.Remove(existingConference);
                    }
                }
            }
            
            return await _context.SaveChangesAsync();
        }

        private IQueryable<Publication> GetAllAsIQueryable() => _context.Publications
            .Include(s => s.Authors)
            .Include(s => s.CitationIndices)
            .Include(s => s.Conference)
            .OrderBy(p => p.Title);

        private async Task EnsureEntryInContext<T>(T entry) where T : class, IIdentifiableT
        {
            T? existingEntry = await _context.FindAsync<T>(entry.Id);

            if (existingEntry == null)
            {
                await _context.AddAsync(entry);
            }
            else
            {
                _context.Entry(existingEntry).CurrentValues.SetValues(entry);
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
