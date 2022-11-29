using System.Text.Json.Serialization;

namespace DocumentApp.Domain
{
    public enum Indexator
    {
        RSCI,
        ELibrary,
        Scopus,
        WebOfScience
    }

    public class CitationIndex : IIdentifiableT
    {
        public Guid Id { get; set; }
        public Indexator Indexator { get; set; }
        public Uri? URL { get; set; }

        public Guid PublicationId { get; set; }

        [JsonIgnore] public Publication Publication { get; set; } = null!;
    }
}
