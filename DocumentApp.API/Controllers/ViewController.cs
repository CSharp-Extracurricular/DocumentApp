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
    // TODO: найти способ попарно объединить перегруженные методы.
    public class ViewController : ControllerBase
    {
        private readonly PublicationRepository _publicationRepository;

        public ViewController(Context context) => _publicationRepository = new PublicationRepository(context);

        // GET: api/View
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicationDto>>> GetAllPublicationsAsync() => await ProceedViewRequest();

        // GET: api/View/filter/
        [HttpGet("filter/")]
        public async Task<ActionResult<IEnumerable<PublicationDto>>> GetFilteredPublicationsAsync([FromQuery] PublicationQuery query)
            => await ProceedViewRequest(query);

        // GET: api/View/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicationDto>> GetPublicationAsync(Guid id) => await ProceedViewRequest(id);

        [NonAction]
        private async Task<ActionResult<IEnumerable<PublicationDto>>> ProceedViewRequest(PublicationQuery? query = null)
        {
            IEnumerable<Publication> result = (query != null)
                ? await _publicationRepository.GetAllAsync(query) 
                : await _publicationRepository.GetAllAsync();

            return GetViewRequestResultFor(result);
        }

        [NonAction]
        private async Task<ActionResult<PublicationDto>> ProceedViewRequest(Guid id)
        {
            Publication? publication = await _publicationRepository.GetByIdAsync(id);

            return GetViewRequestResultFor(publication);
        }

        [NonAction]
        private ActionResult<IEnumerable<PublicationDto>> GetViewRequestResultFor(IEnumerable<Publication> collection)
        {
            return collection.Any()
                ? Ok(GetTranslatedResult(collection))
                : NoContent();
        }

        [NonAction]
        private ActionResult<PublicationDto> GetViewRequestResultFor(Publication? publication)
        {
            return (publication != null)
                ? Ok(GetTranslatedResult(publication))
                : NoContent();
        }

        [NonAction]
        private static IEnumerable<PublicationDto> GetTranslatedResult(IEnumerable<Publication> collection)
        {
            List<PublicationDto> translatedResult = new();

            foreach (Publication publication in collection)
            {
                translatedResult.Add(GetTranslatedResult(publication));
            }

            return translatedResult;
        }

        [NonAction]
        private static PublicationDto GetTranslatedResult(Publication publication) => DtoConverter.Convert(publication);
    }
}
