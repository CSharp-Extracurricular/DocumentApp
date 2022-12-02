using Microsoft.AspNetCore.Mvc; //на вывод.
using DocumentApp.Infrastructure;
using DocumentApp.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocumentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : ControllerBase
    {
        //private readonly Context _context;
        private readonly PublicationRepository _publicationRepository;


        // GET: api/<ViewController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publication>?>> GetPublications()
        {
            //return await _context.Publications.ToListAsync();
            return await _publicationRepository.GetAllAsync();
        }

        // GET api/<ViewController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publication>> GetPublication(Guid id)
        {
            //var publication = await _context.Publications.FindAsync(id);
            var publication = await _publicationRepository.GetByIdAsync(id);
            if (publication == null)
            {
                return NotFound();
            }
            return publication;
        }
    }
}
