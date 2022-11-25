using Microsoft.AspNetCore.Mvc;
using DocumentApp.Infrastructure;
using DocumentApp.Domain;

namespace DocumentApp.API.Controllers
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
            _publicationRepository = new(_context);
        }

        // GET: api/Publication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publication>>> GetPublications() => await _publicationRepository.GetAllAsync();

        // GET: api/Publication/filter/year/5
        [HttpGet("filter/year/{year}")]
        public async Task<ActionResult<IEnumerable<Publication>?>> FilterPublicationsByYear(int year) => await _publicationRepository.GetAllAsyncFilterWith(PublicationFilter.HasSame(year));

        // GET: api/Publication/filter/type/5
        [HttpGet("filter/type/{type}")]
        public async Task<ActionResult<IEnumerable<Publication>?>> FilterPublicationsByType(PublicationType type) => await _publicationRepository.GetAllAsyncFilterWith(PublicationFilter.HasSame(type));

        // GET: api/Publication/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publication>> GetPublication(Guid id)
        {
            Publication? publication = await _publicationRepository.GetByIdAsync(id);
            
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

            if (!PublicationExists(id))
            {
                return NotFound();
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
            if (!PublicationExists(id))
            {
                return NotFound(id);
            }
            
            await _publicationRepository.DeleteByIdAsync(id);

            return NoContent();
        }

        private bool PublicationExists(Guid id) => _publicationRepository.GetByIdAsync(id) != null;
    }
}