using Microsoft.AspNetCore.Mvc;
using DocumentApp.Infrastructure;
using DocumentApp.DTO;

namespace DocumentApp.API.Controllers
{
    /// <summary>
    /// Класс контроллера, содержащий методы API для ввода, изменения и удаления информации.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EditController : ControllerBase
    {
        private readonly PublicationRepository _publicationRepository;

        public EditController(Context context) => _publicationRepository = new PublicationRepository(context) ?? throw new ArgumentNullException(nameof(context));

        // PUT: api/Edit/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutPublication(Guid id, PublicationDto publicationDto)
        {
            if (id != publicationDto.Id)
            {
                return BadRequest();
            }

            if (!await IsPublicationExist(id))
            {
                return NotFound(id);
            }

            await _publicationRepository.UpdateAsync(DtoConverter.ConvertToNative(publicationDto));

            return NoContent();
        }

        // POST: api/Edit
        [HttpPost]
        public async Task<ActionResult<PublicationDto>> PostPublications(PublicationDto publicationDto)
        {
            await _publicationRepository.AddAsync(DtoConverter.ConvertToNative(publicationDto));
            return Created($"/api/View/{publicationDto.Id}", publicationDto);
        }

        // DELETE: api/Edit/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePublication(Guid id)
        {
            if (!await IsPublicationExist(id))
            {
                return NotFound(id);
            }

            await _publicationRepository.DeleteByIdAsync(id);

            return NoContent();
        }

        private async Task<bool> IsPublicationExist(Guid id) => await _publicationRepository.GetByIdAsync(id) != null;
    }
}
