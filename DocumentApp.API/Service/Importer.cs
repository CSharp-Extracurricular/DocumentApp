using DocumentApp.Domain;
using DocumentApp.Infrastructure;

namespace DocumentApp.API
{
    public class Importer
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _exporterUri;
        private readonly Context _context;
        private readonly Security _security;
        public Importer(Uri uri, Context context)
        {
            _httpClient = new HttpClient();
            _exporterUri = uri;
            _httpClient.BaseAddress = _exporterUri;
            _context = context;
        }

        public async Task Import()
        {
            Publication publication = await _httpClient.GetFromJsonAsync<Publication>(_exporterUri) ?? null!;
            
            if (publication == null)
            {
                throw new ArgumentNullException(nameof(publication));
            }
            else
            {
                publication.UserId = _security.GetUserId();
                var _publicationRepository = new PublicationRepository(_context);
                await _publicationRepository.UpdateAsync(publication);
            }
        }
    }
}
