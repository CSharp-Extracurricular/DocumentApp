using DocumentApp.Domain;

namespace DocumentApp.Infrastructure
{
    public class PublicationQuery
    {
        public int? StartYear { get; set; } = null;
        public int? EndYear { get; set; } = null;
        public PublicationType? PublicationType { get; set; } = null;
        public PublicationStatus? PublicationStatus { get; set; } = null;
        public PublicationQuery() { }
    }
}
