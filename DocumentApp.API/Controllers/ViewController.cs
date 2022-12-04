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
        public async Task<ActionResult<IEnumerable<PublicationDto>>> GetPublications() => await ProceedViewRequest();

        // GET: api/View/filter/
        [HttpGet("filter/")]
        public async Task<ActionResult<IEnumerable<PublicationDto>>> GetPulicationsWithFilter(int? startYear, int? endYear, PublicationType? type)
            => await ProceedViewRequest(new PublicationQuery 
                { 
                    StartYear = startYear, 
                    EndYear = endYear, 
                    PublicationType = type 
                });

        // GET: api/View/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicationDto>> GetPublication(Guid id)
        {
            Publication? publication = await _publicationRepository.GetByIdAsync(id);

            return GetViewRequestResultFor(publication);
        }

        private async Task<ActionResult<IEnumerable<PublicationDto>>> ProceedViewRequest(PublicationQuery? query = null)
        {
            IEnumerable<Publication> result = query.HasValue 
                ? await _publicationRepository.GetAllAsync(query.Value) 
                : await _publicationRepository.GetAllAsync();

            return GetViewRequestResultFor(result);
        }

        private ActionResult<IEnumerable<PublicationDto>> GetViewRequestResultFor(IEnumerable<Publication> collection)
        {
            if (!collection.Any())
            {
                return NoContent();
            }
            else
            {
                return Ok(GetTranslatedResults(collection));
            }
        }

        private ActionResult<PublicationDto> GetViewRequestResultFor(Publication? publication)
        {
            if (publication == null)
            {
                return NotFound();
            }

            return Ok(DtoConverter.Translate(publication));
        }

        private static IEnumerable<PublicationDto> GetTranslatedResults(IEnumerable<Publication> collection)
        {
            List<PublicationDto> translatedResult = new();

            foreach (Publication publication in collection)
            {
                translatedResult.Add(DtoConverter.Translate(publication));
            }

            return translatedResult;
        }
    }
}
