using DocumentApp.Domain;
using System.Linq.Expressions;

namespace DocumentApp.Infrastructure
{
    public static class PublicationFilter
    {
        public static Expression<Func<Publication, bool>> HasSame(int year) => publication => publication.PublishingYear == year;
    
        public static Expression<Func<Publication, bool>> HasSame(PublicationType type) => publication => publication.PublicationType == type;
    }
}
