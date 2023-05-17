using System.Web;
using Microsoft.AspNetCore.Mvc;
using DocumentApp.Infrastructure;
using DocumentApp.Domain;

namespace DocumentApp.API.Controllers;

/// <summary>
/// Класс контроллера, содержащий методы для импорта публикаций из другой системы.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ImportController : ControllerBase
{
    private readonly Context _context;
    private readonly ISecurity _security;

    public ImportController(Context context)
    {
        _context = context;
        _security = new MockSecurityProvider();
    }

    // GET api/Import/5
    [HttpGet("{uri}")]
    public async Task<IActionResult> GetPublication(string uri)
    {
        try
        {
            Uri uriParsed = new(uri.Contains('%') ? HttpUtility.UrlDecode(uri) : uri);
            Importer importer = new(uriParsed, _context, _security.GetUserId());
            await importer.ImportAsync();

            return NoContent();
        }
        catch (Exception exception) 
        {
            return NotFound(exception.Message);
        }
    }

    // GET api/Import/update/5
    [HttpGet("update/{id:guid}")]
    public async Task<IActionResult> UpdatePublicationFromImportId(Guid id)
    {
        PublicationRepository publicationRepository = new (_context);
        Publication? publication = await publicationRepository.GetByIdAsync(id);

        if (publication != null && publication.ImportUri != null)
        {
            _ = await GetPublication(publication.ImportUri.ToString());
        }

        return NoContent();
    }
}