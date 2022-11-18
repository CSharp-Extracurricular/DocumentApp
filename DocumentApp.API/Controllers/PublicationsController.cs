﻿using Microsoft.AspNetCore.Mvc;
using DocumentApp.Infrastructure;
using DocumentApp.Domain;

namespace DocumentApp.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationsController : ControllerBase
    {
        private readonly Context _context;
        private readonly PublicationRepository _publicationRepository;

        public PublicationsController(Context context)
        {
            _context = context;
            _publicationRepository = new PublicationRepository(_context);
        }

        // GET: api/Publication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publication>>> GetPublications()
        {
            //return await _context.Publications.ToListAsync();
            return await _publicationRepository.GetAllAsync();
        }

        // GET: api/Publication/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publication>> GetPublication(Guid id)
        {
            var publication = await _context.Publications.FindAsync(id);
            //var publication = await _publicationRepository.GetByIdAsync(id);
            if (publication == null)
            {
                return NotFound();
            }
            return publication;
        }

        // PUT: api/Publication/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublication(Guid id, Publication publication)
        {
            if (id != publication.Id)
            {
                return BadRequest();
            }
            await _publicationRepository.UpdateAsync(publication);

            return NoContent();
        }

        // POST: api/Publication
        [HttpPost]
        public async Task<ActionResult<Publication>> PostPublications(Publication publication)
        {
            await _publicationRepository.AddAsync(publication);
            return CreatedAtAction("GetPublications", new { id = publication.Id }, publication);
        }

        // DELETE: api/Publication/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublication(Guid id)
        {
            var publication = await _publicationRepository.GetByIdAsync(id);
            if (publication == null)
            {
                return NotFound();
            }

            _context.Publications.Remove(publication);
            await _context.SaveChangesAsync();
            //await _publicationRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
