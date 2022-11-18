using System.Text.Json.Serialization;

namespace DocumentApp.Domain
{
    public enum ConferenceType
    {
        Local,
        Regional,
        National,
        International
    }

    public class Conference : IIdentifiableT
    {
        public Guid Id { get; set; }
        public string ShortName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ConferenceType Type { get; set; }
        public string Location { get; set; } = string.Empty;

        public Guid PublicationId { get; set; }

        [JsonIgnore] public Publication Publication { get; set; } = null!;
    }
}