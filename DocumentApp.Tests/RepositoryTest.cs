using DocumentApp.Domain;

namespace DocumentApp.Tests
{
    public class RepositoryTest
    {
        public readonly TestHelper MainTestHelper = new();

        private Infrastructure.PublicationRepository TestRepository => MainTestHelper.TestRepository;

        [Fact]
        public async void TestAddAsync() => Assert.NotEqual(0, await TestRepository.AddAsync(GetTestPublication()));

        [Fact]
        public async void TestGetByIdAsync()
        {
            Publication publication = GetTestPublication();
            await TestRepository.AddAsync(publication);
            Publication? result = await TestRepository.GetByIdAsync(publication.Id) ?? null!;

            ComparePublications(publication, result);
        }

        [Fact]
        public async void TestDeleteByIdAsync()
        {
            Publication publication = GetTestPublication();
            await TestRepository.AddAsync(publication);

            Assert.NotEqual(0, await TestRepository.DeleteByIdAsync(publication.Id));
        }

        [Fact] 
        public async void TestGetAllAsync()
        {
            Random random = new();
            List<Publication> publications = new();

            for (int i = 0, j = random.Next(2, 10); i < j; i++) publications.Add(GetTestPublication());

            foreach (Publication i in publications) await TestRepository.AddAsync(i);

            List<Publication>? results = await TestRepository.GetAllAsync();

            foreach (Publication i in publications) Assert.Contains(i, results);
        }

        [Fact]
        public async void TestUpdateAsync()
        {
            Publication publication = GetTestPublication();
            await TestRepository.AddAsync(publication);

            publication.Title = Guid.NewGuid().ToString();
            await TestRepository.UpdateAsync(publication);

            Publication? result = await TestRepository.GetByIdAsync(publication.Id) ?? null!;
            ComparePublications(publication, result);

            for (int i = 0; i < 2; i++)
            {
                publication.Authors.Add(GetTestAuthor());
                publication.Indices.Add(GetTestCitationIndex());
            }

            publication.Conference = GetTestConference();

            result = await TestRepository.GetByIdAsync(publication.Id) ?? null!;
            ComparePublications(publication, result);
        }

        private static void ComparePublications(Publication publication, Publication result)
        {
            Assert.Equal(publication, result);
            Assert.Equal(publication.Authors, result.Authors);
            Assert.Equal(publication.Indices, result.Indices);
            Assert.Equal(publication.Conference, result.Conference);
        }

        private static Publication GetTestPublication()
        {
            Random random = new();

            Publication publication = new()
            {
                Id = Guid.NewGuid(),
                Title = Guid.NewGuid().ToString(),
                PublicationType = (PublicationType)random.Next(0, 4),
                PublishingYear = random.Next(1990, 2022)
            };

            for (int i = 0, j = random.Next(2, 4); i < j; i++)
            {
                publication.Authors.Add(GetTestAuthor());
                publication.Indices.Add(GetTestCitationIndex());
            }

            publication.Conference = GetTestConference();

            return publication;
        }

        private static Author GetTestAuthor()
        {
            Random random = new();

            return new Author()
            {
                Id = Guid.NewGuid(),

                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                PatronimicName = Guid.NewGuid().ToString(),
                Number = random.Next(0, 10)
            };
        }

        private static Conference GetTestConference()
        {
            Random random = new();

            return new Conference()
            {
                Id = Guid.NewGuid(),
                ShortName = Guid.NewGuid().ToString(),
                FullName = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Type = (ConferenceType)random.Next(0, 3),
                Location = Guid.NewGuid().ToString()
            };
        }

        private static CitationIndex GetTestCitationIndex()
        {
            Random random = new();

            return new CitationIndex()
            {
                Id = Guid.NewGuid(),
                Indexator = (Indexator)random.Next(0, 3),
                URL = Guid.NewGuid().ToString()
            };
        }
    }
}