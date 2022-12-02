using DocumentApp.Domain;

namespace DocumentApp.Infrastructure
{
    public struct PublicationQuery
    {
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public PublicationType? PublicationType { get; set; }
    }
}
