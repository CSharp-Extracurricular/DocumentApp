using DocumentApp.Domain;

namespace DocumentApp.DTO
{
    public static class DtoConverter
    {
        public static PublicationDto Convert(Publication publication)
        {
            PublicationDto result = new()
            {
                Id = publication.Id,
                Title = publication.Title,
                PublishingYear = publication.PublishingYear,
                PublicationType = publication.PublicationType,
                PublicationStatus = publication.PublicationStatus,
                DOI = publication.DOI,
                AuthorGroupId = publication.AuthorGroupId,
                UserId = publication.UserId,
                Authors = ConvertList(publication.Authors),
                CitationIndices = ConvertList(publication.CitationIndices)
            };

            if (publication.Conference != null)
            {
                result.Conference = Convert(publication.Conference);
            }

            return result;
        }

        public static List<AuthorDto> ConvertList(List<Author> list)
        {
            List<AuthorDto> result = new();

            foreach (Author author in list)
            {
                result.Add(Convert(author));
            }

            return result;
        }

        public static List<CitationIndexDto> ConvertList(List<CitationIndex> list)
        {
            List<CitationIndexDto> result = new();

            foreach (CitationIndex index in list)
            {
                result.Add(Convert(index));
            }

            return result;
        }

        public static AuthorDto Convert(Author author)
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

        public static CitationIndexDto Convert(CitationIndex index)
        {
            return new CitationIndexDto()
            {
                Id = index.Id,
                Indexator = index.Indexator,
                URL = index.URL,
                PublicationId = index.PublicationId
            };
        }

        public static ConferenceDto Convert(Conference conference)
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

        public static Publication ConvertToNative(PublicationDto publicationdto)
        {
            Publication result = new()
            {
                Id = publicationdto.Id,
                Title = publicationdto.Title,
                PublishingYear = publicationdto.PublishingYear,
                PublicationType = publicationdto.PublicationType,
                PublicationStatus = publicationdto.PublicationStatus,
                DOI = publicationdto.DOI,
                AuthorGroupId = publicationdto.AuthorGroupId,
                UserId = publicationdto.UserId,
                Authors = ConvertListToNative(publicationdto.Authors),
                CitationIndices = ConvertListToNative(publicationdto.CitationIndices)
            };

            foreach (Author author in result.Authors)
            {
                author.Publication = result;
            }

            foreach (CitationIndex index in result.CitationIndices)
            {
                index.Publication = result;
            }

            if (publicationdto.Conference != null)
            {
                result.Conference = ConvertToNative(publicationdto.Conference);
                result.Conference.Publication = result;
            }

            return result;
        }

        public static List<Author> ConvertListToNative(List<AuthorDto> list)
        {
            List<Author> result = new();

            foreach (AuthorDto author in list)
            {
                result.Add(ConvertToNative(author));
            }

            return result;
        }

        public static Author ConvertToNative(AuthorDto authorDto)
        {
            return new Author()
            {
                Id = authorDto.Id,
                ObjectId = authorDto.ObjectId,
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                PatronimicName = authorDto.PatronimicName,
                Email = authorDto.Email,
                Number = authorDto.Number,
                PublicationId = authorDto.PublicationId
            };
        }

        public static List<CitationIndex> ConvertListToNative(List<CitationIndexDto> list)
        {
            List<CitationIndex> result = new();

            foreach (CitationIndexDto index in list)
            {
                result.Add(ConvertToNative(index));
            }

            return result;
        }

        public static CitationIndex ConvertToNative(CitationIndexDto citationIndexDto)
        {
            return new CitationIndex()
            {
                Id = citationIndexDto.Id,
                Indexator = citationIndexDto.Indexator,
                URL = citationIndexDto.URL,
                PublicationId = citationIndexDto.PublicationId
            };
        }

        public static Conference ConvertToNative(ConferenceDto conferenceDto)
        {
            return new Conference()
            {
                Id = conferenceDto.Id,
                ShortName = conferenceDto.ShortName,
                FullName = conferenceDto.FullName,
                StartDate = conferenceDto.StartDate,
                EndDate = conferenceDto.EndDate,
                Type = conferenceDto.Type,
                Location = conferenceDto.Location,
                PublicationId = conferenceDto.PublicationId
            };
        }
    }
}
