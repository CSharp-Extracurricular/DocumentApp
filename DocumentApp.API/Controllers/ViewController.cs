using Microsoft.AspNetCore.Mvc; //на вывод.
using DocumentApp.Infrastructure;
using DocumentApp.Domain;

namespace DocumentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : ControllerBase
    {
        private readonly PublicationRepository _publicationRepository;

        public ViewController(Context context) => _publicationRepository = new PublicationRepository(context);

        // GET: api/View
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publication>>> GetPublications() => await _publicationRepository
            .GetAllAsync();

        // GET: api/Publication/filter/
        [HttpGet("filter/{query}")]
        public async Task<ActionResult<IEnumerable<Publication>>> PublicationFilter(PublicationQuery query) => await _publicationRepository
            .GetAllAsyncFiltered(query);

        // GET: api/Publication/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publication>> GetPublication(Guid id)
        {
            Publication? publication = await _publicationRepository.GetByIdAsync(id);

            if (publication == null)
            {
                return NotFound();
            }

            return Ok(publication);
        }
    }
}
