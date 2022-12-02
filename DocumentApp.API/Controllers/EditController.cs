using Microsoft.AspNetCore.Mvc; //ввод, изменение и удаление. 
using DocumentApp.Infrastructure;
using DocumentApp.Domain;

namespace DocumentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditController : ControllerBase
    {
        private readonly PublicationRepository _publicationRepository;

        public EditController(Context context) => _publicationRepository = new PublicationRepository(context);

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
                return NotFound(id);
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
