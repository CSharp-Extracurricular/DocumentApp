using Microsoft.AspNetCore.Mvc;
using DocumentApp.Infrastructure;
using DocumentApp.Domain;
using DocumentApp.DTO;

namespace DocumentApp.API.Controllers
{
    /// <summary>
    /// Класс контроллера, содержащий методы API для вывода информации.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : ControllerBase
    {
        private readonly PublicationRepository _publicationRepository;

        public ViewController(Context context) => _publicationRepository = new PublicationRepository(context);

        // GET: api/View
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicationDto>>> GetPublications()
        {
            List<Publication> result = await _publicationRepository.GetAllAsync();

            if (result.Count == 0)
            {
                return NoContent();
            }
            else
            {
                List<PublicationDto> translatedResult = new();

                foreach (Publication publication in result)
                {
                    translatedResult.Add(DtoConverter.Translate(publication));
                }

                return Ok(translatedResult);
            }
        }

        // GET: api/View/filter/
        [HttpGet("filter/{query}")]
        public async Task<ActionResult<IEnumerable<PublicationDto>>> PublicationFilter(PublicationQuery query)
        {
            List<Publication> result = await _publicationRepository.GetAllAsyncFiltered(query);

            if (result.Count == 0)
            {
                return NoContent();
            }
            else
            {
                List<PublicationDto> translatedResult = new();

                foreach (Publication publication in result)
                {
                    translatedResult.Add(DtoConverter.Translate(publication));
                }

                return Ok(translatedResult);
            }
        }

        // GET: api/View/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicationDto>> GetPublication(Guid id)
        {
            Publication? publication = await _publicationRepository.GetByIdAsync(id);

            if (publication == null)
            {
                return NotFound();
            }

            return Ok(DtoConverter.Translate(publication));
        }
    }
}
