using DocumentApp.Domain;
using DocumentApp.Infrastructure;

namespace DocumentApp.API
{
    public class Importer
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _exporterUri;
        private readonly ISecurity _security;
        private readonly PublicationRepository _publicationRepository;

        public Importer(Uri uri, Context context)
        {
            _httpClient = new HttpClient();
            _exporterUri = uri;
            _httpClient.BaseAddress = _exporterUri;
            _publicationRepository = new PublicationRepository(context);
            _security = new MockSecurityProvider();
        }

        public async Task ImportAsync()
        {
            Publication publication = await _httpClient.GetFromJsonAsync<Publication>(_exporterUri) ?? null!;
            
            if (publication == null)
            {
                throw new ArgumentNullException(nameof(publication));
            }
            else
            {
                publication.UserId = _security.GetUserId();
                await _publicationRepository.UpdateAsync(publication);
            }
        }
    }
}
