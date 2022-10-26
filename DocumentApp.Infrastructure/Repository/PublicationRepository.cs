using DocumentApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace DocumentApp.Infrastructure.Repository
{
    public class PublicationRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public async Task<Publication?> GetByIdAsync(Guid id)
        {
            return await _context.Publications
                .Where(a => a.Id == id)
                .Include(s => s.Authors)
                .Include(b => b.Conference)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Publication publication)
        {
            _context.Publications.Add(publication);
            await _context.SaveChangesAsync();
        }
    }
}
