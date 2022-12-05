using DocumentApp.Domain;

namespace DocumentApp.Infrastructure
{
    public class PublicationQuery
    {
        public int? StartYear { get; set; } = null;
        public int? EndYear { get; set; } = null;
        public PublicationType? PublicationType { get; set; } = null;

        public PublicationQuery()
        {
            StartYear = null;
            EndYear = null;
            PublicationType = null;
        }

        public PublicationQuery(int? startYear, int? endYear, PublicationType? type)
        {
            StartYear = startYear;
            EndYear = endYear;
            PublicationType = type;
        }
    }
}
