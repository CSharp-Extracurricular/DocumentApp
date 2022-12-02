using DocumentApp.Domain;

namespace DocumentApp.DTO
{
    public class CitationIndexDto
    {
        public Guid Id { get; set; }
        public Indexator Indexator { get; set; }
        public Uri URL { get; set; } = null!;

        public Guid PublicationId { get; set; }
    }
}
