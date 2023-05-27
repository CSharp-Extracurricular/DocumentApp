namespace DocumentApp.Domain
{
    public enum PublicationType
    {
        Monography,
        Textbook,
        Article,
        Report,
        Thesis
    }

    public enum PublicationStatus
    {
        Черновик,
        Отправлено,
        Доработка,
        Принято,
        Опубликовано
    }

    public class Publication : IIdentifiableT
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int PublishingYear { get; set; }
        public PublicationType PublicationType { get; set; }

        public PublicationStatus PublicationStatus { get; set; }

        // Цифровой идентификатор объекта.
        public string DOI { get; set; } = string.Empty;

        // Идентификатор для группировки идентичных объектов.
        public Guid AuthorGroupId { get; set; }

        // Идентификатор создателя записи.
        public Guid UserId { get; set; }

        public Uri? ImportUri { get; set; }

        public List<Author> Authors { get; set; } = new List<Author>();
        public List<CitationIndex> CitationIndices { get; set; } = new List<CitationIndex>();
        public Conference? Conference { get; set; }
    }
}
