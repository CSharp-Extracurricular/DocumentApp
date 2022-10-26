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

        public async Task AddAsync(Publication publication)
        {
            _context.Publications.Add(publication);
            await _context.SaveChangesAsync();
        }
    }
}
