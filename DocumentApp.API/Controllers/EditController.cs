﻿using Microsoft.AspNetCore.Mvc;
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

        public EditController(Context context) => _publicationRepository = new PublicationRepository(context);

        // PUT: api/Edit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublication(Guid id, PublicationDto publicationDto)
        {
            if (id != publicationDto.Id)
            {
                return BadRequest();
            }

            if (!PublicationExists(id))
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
            return CreatedAtAction("GetAllPublicationsAsync", new { id = publicationDto.Id }, publicationDto);
        }

        // DELETE: api/Edit/5
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
