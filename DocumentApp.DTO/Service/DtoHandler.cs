using DocumentApp.Domain;

namespace DocumentApp.DTO
{
    public static class DtoHandler
    {
        public static PublicationDto Translate(Publication publication)
        {
            PublicationDto result = new()
            {
                Id = publication.Id,
                Title = publication.Title,
                PublishingYear = publication.PublishingYear,
                PublicationType = publication.PublicationType,
                DOI = publication.DOI,
                AuthorGroupId = publication.AuthorGroupId,
                UserId = publication.UserId,
                Authors = TranslateList(publication.Authors),
                CitationIndices = TranslateList(publication.CitationIndices)
            };

            if (publication.Conference != null) 
            { 
                result.Conference = Translate(publication.Conference); 
            }

            return result;
        }

        public static List<AuthorDto> TranslateList(List<Author> list)
        {
            List<AuthorDto> result = new();
            
            foreach (Author author in list)
            {
                result.Add(Translate(author));
            }

            return result;
        }

        public static List<CitationIndexDto> TranslateList(List<CitationIndex> list)
        {
            List<CitationIndexDto> result = new();

            foreach (CitationIndex index in list)
            {
                result.Add(Translate(index));
            }

            return result;
        }

        public static AuthorDto Translate(Author author)
        {
            return new AuthorDto()
            {
                Id = author.Id,
                ObjectId = author.ObjectId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                PatronimicName = author.PatronimicName,
                Email = author.Email,
                Number = author.Number,
                PublicationId = author.PublicationId
            };
        }

        public static CitationIndexDto Translate(CitationIndex index)
        {
            return new CitationIndexDto()
            {
                Id = index.Id,
                Indexator = index.Indexator,
                URL= index.URL,
                PublicationId = index.PublicationId
            };
        }

        public static ConferenceDto Translate(Conference conference)
        {
            return new ConferenceDto()
            {
                Id = conference.Id,
                ShortName = conference.ShortName,
                FullName = conference.FullName,
                StartDate = conference.StartDate,
                EndDate = conference.EndDate,
                Type = conference.Type,
                Location = conference.Location,
                PublicationId = conference.PublicationId
            };
        }
    }
}
