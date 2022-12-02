using DocumentApp.Domain;

namespace DocumentApp.DTO
{
    public class ConferenceDto
    {
        public Guid Id { get; set; }
        public string ShortName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ConferenceType Type { get; set; }
        public string Location { get; set; } = string.Empty;

        public Guid PublicationId { get; set; }
    }
}
