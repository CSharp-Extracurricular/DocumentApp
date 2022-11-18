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
        public string URL { get; set; } = string.Empty;
    }
}
