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

        // GET api/Import/5
        [HttpGet("{id}")]
        public async Task GetPublication(Uri uri)
        {
            Importer importer = new(uri, _context);
            await importer.Import();
        }
    }
}