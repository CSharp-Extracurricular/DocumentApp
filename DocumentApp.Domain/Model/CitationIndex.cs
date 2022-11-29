using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public Uri URL { get; set; } = null!;

        public Guid PublicationId { get; set; }

        [ValidateNever] [JsonIgnore] public Publication Publication { get; set; } = null!;
    }
}
