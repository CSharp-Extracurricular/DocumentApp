using DocumentApp.Domain;

namespace DocumentApp.DTO
{
    public class PublicationDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int PublishingYear { get; set; }
        public PublicationType PublicationType { get; set; }

        // Цифровой идентификатор объекта.
        public string DOI { get; set; } = string.Empty;

        // Идентификатор для группировки идентичных объектов.
        public Guid AuthorGroupId { get; set; }

        // Идентификатор создателя записи.
        public Guid UserId { get; set; }

        public List<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
        public List<CitationIndexDto> CitationIndices { get; set; } = new List<CitationIndexDto>();
        public ConferenceDto? Conference { get; set; }
    }
}
