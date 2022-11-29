using DocumentApp.Infrastructure;
using DocumentApp.Domain;

namespace DocumentApp.Tests
{
    public class RepositoryTest
    {
        private static readonly Random random = new();

        private readonly TestHelper MainTestHelper;
        private PublicationRepository TestRepository => MainTestHelper.TestRepository;

        public RepositoryTest() { MainTestHelper = new(); }

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
        }

        [Fact]
        public async void TestUpdateAddAsync()
        {
            Publication publication = GetTestPublication();
            await TestRepository.AddAsync(publication);
            
            for (int i = 0; i < 2; i++)
            {
                publication.Authors.Add(GetTestAuthor());
                publication.CitationIndices.Add(GetTestCitationIndex());
            }

            publication.Authors[0] = GetTestAuthor();
            publication.CitationIndices[0] = GetTestCitationIndex();
            publication.Conference = GetTestConference();

            await TestRepository.UpdateAsync(publication);

            Publication? result = await TestRepository.GetByIdAsync(publication.Id);
            Assert.NotNull(result);
            ComparePublications(publication, result ?? null!);
        }

        private static void ComparePublications(Publication publication, Publication result)
        {
            Assert.Equal(publication, result);
            Assert.Equal(publication.Authors, result.Authors);
            Assert.Equal(publication.CitationIndices, result.CitationIndices);
            Assert.Equal(publication.Conference, result.Conference);
        }

        private static Publication GetTestPublication()
        {
            Publication publication = new()
            {
                Title = "Test case",
                PublicationType = 0,
                PublishingYear = 1994
            };

            for (int i = 0, j = random.Next(1, 3); i < j; i++)
            {
                publication.Authors.Add(GetTestAuthor());
                publication.CitationIndices.Add(GetTestCitationIndex());
            }

            publication.Conference = GetTestConference();

            return publication;
        }

        private static Author GetTestAuthor()
        {
            return new Author()
            {
                FirstName = "Test case",
                LastName = "Test case",
                PatronimicName = "Test case",
                Number = 1
            };
        }

        private static Conference GetTestConference()
        {
            return new Conference()
            {
                ShortName = "Test case",
                FullName = "Test case",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Type = ConferenceType.International,
                Location = "Test case"
            };
        }

        private static CitationIndex GetTestCitationIndex()
        {
            return new CitationIndex()
            {
                Indexator = Indexator.ELibrary,
                URL = new Uri("https://learn.microsoft.com/ru-ru/dotnet/api/system.uri?view=net-7.0")
            };
        }
    }
}