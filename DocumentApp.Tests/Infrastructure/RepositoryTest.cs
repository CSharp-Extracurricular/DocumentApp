namespace DocumentApp.Tests
{
    [Collection("MainTestCollection")]
    public class RepositoryTest
    {   
        private PublicationRepository TestRepository { get; }

        public RepositoryTest() 
        {
            TestHelper MainTestHelper = new();
            TestRepository = MainTestHelper.TestRepository;
        }

        [Fact]
        public async Task TestAddAsync() => Assert.NotEqual(0, await TestRepository.AddAsync(PublicationsFactory.FirstPublication));

        [Fact]
        public async Task TestGetByIdAsync()
        {
            Publication publication = PublicationsFactory.FirstPublication;
            await TestRepository.AddAsync(publication);
            Publication? result = await TestRepository.GetByIdAsync(PublicationsFactory.FirstPublicationId) ?? null!;

            ComparePublications(publication, result);
        }

        [Fact]
        public async Task TestDeleteByIdAsync()
        {
            Publication publication = PublicationsFactory.FirstPublication;
            await TestRepository.AddAsync(publication);

            Assert.NotEqual(0, await TestRepository.DeleteByIdAsync(PublicationsFactory.FirstPublicationId));
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            List<Publication> publications = new()
            {
                PublicationsFactory.FirstPublication,
                PublicationsFactory.SecondPublication,
                PublicationsFactory.ThirdPublication
            };

            foreach (Publication i in publications)
            {
                await TestRepository.AddAsync(i);
            }

            List<Publication>? results = await TestRepository.GetAllAsync();

            foreach (Publication i in publications)
            {
                Assert.Contains(i, results);
            }
        }

        [Fact]
        public async Task TestUpdateAsync()
        {
            Publication publication = PublicationsFactory.FirstPublication;
            await TestRepository.AddAsync(publication);
            publication.Title = "Changed publication title";

            await TestRepository.UpdateAsync(publication);

            Publication? result = await TestRepository.GetByIdAsync(publication.Id) ?? null!;
            ComparePublications(publication, result);
        }

        [Fact]
        public async Task TestUpdateAddAuthorAsync()
        {
            Publication publication = PublicationsFactory.FirstPublication;
            await TestRepository.AddAsync(publication);

            publication.Authors.Add(new Author()
            {
                FirstName = "Test case 1",
                LastName = "Test case 1",
                PatronimicName = "Test case",
                Email = "example@example.com",
                Number = 2,
                PublicationId = publication.Id,
                Publication = publication
            });

            await TestRepository.UpdateAsync(publication);

            Publication? result = await TestRepository.GetByIdAsync(publication.Id);
            Assert.NotNull(result);
            ComparePublications(publication, result ?? null!);
        }

        public async Task TestUpdateAddIndexAsync()
        {
            Publication publication = PublicationsFactory.FirstPublication;
            await TestRepository.AddAsync(publication);

            publication.CitationIndices.Add(new CitationIndex()
            {
                Indexator = Indexator.ELibrary,
                URL = new Uri("https://learn.microsoft.com/ru-ru/dotnet/api/system.uri?view=net-7.0"),
                PublicationId = publication.Id,
                Publication = publication
            });

            await TestRepository.UpdateAsync(publication);

            Publication? result = await TestRepository.GetByIdAsync(publication.Id);
            Assert.NotNull(result);
            ComparePublications(publication, result ?? null!);
        }

        public async Task TestUpdateAddConferenceAsync()
        {
            Publication publication = PublicationsFactory.FirstPublication;
            await TestRepository.AddAsync(publication);

            publication.Conference = new Conference()
            {
                ShortName = "Test case",
                FullName = "Test case",
                StartDate = new DateTime(2022, 12, 12),
                EndDate = new DateTime(2022, 12, 12),
                Type = ConferenceType.International,
                Location = "Test case",
                PublicationId = publication.Id,
                Publication = publication
            };

            await TestRepository.UpdateAsync(publication);

            Publication? result = await TestRepository.GetByIdAsync(publication.Id);
            Assert.NotNull(result);
            ComparePublications(publication, result ?? null!);
        }

        public async Task TestUpdateRemoveAuthorAsync()
        {
            Publication publication = PublicationsFactory.FirstPublication;
            await TestRepository.AddAsync(publication);

            publication.Authors.RemoveAt(0);

            await TestRepository.UpdateAsync(publication);

            Publication? result = await TestRepository.GetByIdAsync(publication.Id);
            Assert.NotNull(result);
            ComparePublications(publication, result ?? null!);
        }

        public async Task TestUpdateRemoveIndexAsync()
        {
            Publication publication = PublicationsFactory.FirstPublication;
            await TestRepository.AddAsync(publication);

            publication.CitationIndices.RemoveAt(0);

            await TestRepository.UpdateAsync(publication);

            Publication? result = await TestRepository.GetByIdAsync(publication.Id);
            Assert.NotNull(result);
            ComparePublications(publication, result ?? null!);
        }

        public async Task TestUpdateRemoveConferenceAsync()
        {
            Publication publication = PublicationsFactory.FirstPublication;
            await TestRepository.AddAsync(publication);

            publication.Conference = null;

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
    }
}