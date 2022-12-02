using Microsoft.AspNetCore.Mvc;
using DocumentApp.Infrastructure;

namespace DocumentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly Context _context;

        public ImportController(Context context) => _context = context;

        // GET api/ImportAsync/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublication(Uri uri)
        {
            Importer importer = new(uri, _context);

            try
            {
                await importer.ImportAsync();
                return Ok();
            }
            catch (ArgumentNullException exception) 
            {
                return NotFound(exception.ParamName);
            }
        }
    }
}