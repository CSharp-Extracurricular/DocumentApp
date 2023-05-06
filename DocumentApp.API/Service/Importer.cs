using DocumentApp.Domain;
using DocumentApp.DTO;
using DocumentApp.Infrastructure;

namespace DocumentApp.API
{
    public class Importer
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _exporterUri;
        private readonly PublicationRepository _publicationRepository;
        private readonly Guid _personId;

        public Importer(Uri uri, Context context, Guid personId)
        {
            _httpClient = new HttpClient();
            _exporterUri = uri;
            _httpClient.BaseAddress = _exporterUri;
            _publicationRepository = new PublicationRepository(context);
            _personId = personId;
        }

        public async Task ImportAsync()
        {
            PublicationDto? result = await ProceedGetRequestAsync();
            Publication? publication = DtoConverter.ConvertToNative(result);

            await ImportPublicationIfNotNullAsync(publication);
        }

        private async Task ImportPublicationIfNotNullAsync(Publication? publication)
        {
            if (publication == null)
            {
                throw new Exception($"Error: Publication not found on request URI: {_exporterUri}");
            }

            await EnsurePublicationInContextAsync(publication);
        }

        private async Task EnsurePublicationInContextAsync(Publication publication)
        {
            publication.UserId = _personId;
            await _publicationRepository.AddAsync(publication);
        }

        private async Task<PublicationDto?> ProceedGetRequestAsync() 
            => await _httpClient.GetFromJsonAsync<PublicationDto?>(_exporterUri);
    }
}
