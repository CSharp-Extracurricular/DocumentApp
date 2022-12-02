using Microsoft.AspNetCore.Mvc; //ввод, изменение и удаление. 
using DocumentApp.Infrastructure;
using DocumentApp.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocumentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditController : ControllerBase
    {
        private readonly Context _context;
        private readonly PublicationRepository _publicationRepository;

        // POST api/<EditController>
        [HttpPost]
        public async Task<ActionResult<Publication>> PostPublications(Publication publication)
        {
            await _publicationRepository.AddAsync(publication);
            return CreatedAtAction("GetPublications", new { id = publication.Id }, publication);
        }

        // PUT api/<EditController>/5
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

        // DELETE api/<EditController>/5
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

        private bool PublicationExists(Guid id)
        {
            return _context.Publications.Any(e => e.Id == id);
        }
    }
}
