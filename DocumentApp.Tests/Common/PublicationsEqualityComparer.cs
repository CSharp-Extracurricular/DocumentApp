namespace DocumentApp.Tests
{
    public class PublicationsEqualityComparer : IEqualityComparer<Publication>
    {
        public bool Equals(Publication? x, Publication? y)
        {
            return
                x.Id == y.Id
                && x.PublishingYear == y.PublishingYear
                && x.PublicationType == y.PublicationType
                && x.Title == y.Title
                && x.AuthorGroupId == y.AuthorGroupId
                && IsAuthorsListEqual(x.Authors, y.Authors)
                && IsCitationIndicesListEqual(x.CitationIndices, y.CitationIndices)
                && AreConferencesEqual(x.Conference, y.Conference);
        }

        private static bool IsAuthorsListEqual(List<Author> x, List<Author> y)
        {
            if (x.Count != y.Count)
            {
                return false;
            }

            
            for (int i = 0, j = x.Count - 1; i < j; i++)
            {
                if (!AreAuthorsEqual(x.ElementAt(i), y.ElementAt(i)))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool AreAuthorsEqual(Author x, Author y)
        {
            return
                x.Id == y.Id
                && x.FirstName == y.FirstName
                && x.LastName == y.LastName
                && x.Email == y.Email
                && x.Number == y.Number
                && x.PatronimicName == y.PatronimicName
                && x.PublicationId == y.PublicationId
                && x.ObjectId == y.ObjectId;
        }

        private static bool IsCitationIndicesListEqual(List<CitationIndex> x, List<CitationIndex> y)
        {
            if (x.Count != y.Count)
            {
                return false;
            }


            for (int i = 0, j = x.Count - 1; i < j; i++)
            {
                if (!AreCitationIndicesEqual(x.ElementAt(i), y.ElementAt(i)))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool AreCitationIndicesEqual(CitationIndex x, CitationIndex y)
        {
            return
                x.Id == y.Id
                && x.Indexator == y.Indexator
                && x.URL == y.URL
                && x.PublicationId == y.PublicationId;
        }

        private static bool AreConferencesEqual(Conference? x, Conference? y)
        {
            // Returns false if both conferences are null either have values.
            if (x == null ^ y == null)
            {
                return false;
            }
            else if (x == null && y == null)
            {
                return true;
            }
            else
            {
                return
                    x.Id == y.Id
                    && x.ShortName == y.ShortName
                    && x.FullName == y.FullName
                    && x.StartDate == y.StartDate
                    && x.EndDate == y.EndDate
                    && x.Type == y.Type
                    && x.Location == y.Location
                    && x.PublicationId == y.PublicationId;
            }
        }

        public int GetHashCode(Publication obj)
        {
            throw new NotImplementedException();
        }
    }
}
