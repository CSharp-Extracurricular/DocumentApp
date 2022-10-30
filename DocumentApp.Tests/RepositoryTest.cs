using DocumentApp.Domain;

namespace DocumentApp.Tests
{
    public class RepositoryTest
    {
        public TestHelper MainTestHelper = new();

        private Infrastructure.PublicationRepository TestRepository => MainTestHelper.TestRepository;

        [Fact]
        public async void TestAddAsync() => Assert.Equal(1, await TestRepository.AddAsync(GetTestPublication()));

        [Fact]
        public async void TestGetByIdAsync()
        {
            Publication publication = GetTestPublication();
            await TestRepository.AddAsync(publication);
            Publication? result = await TestRepository.GetByIdAsync(publication.Id) ?? null!;

            Assert.Equal(publication, result);
        }

        [Fact]
        public async void TestDeleteByIdAsync()
        {
            Publication publication = GetTestPublication();
            await TestRepository.AddAsync(publication);

            Assert.Equal(1, await TestRepository.DeleteByIdAsync(publication.Id));
        }

        [Fact] 
        public async void TestGetAllAsync()
        {
            Random random = new();
            List<Publication> publications = new();

            for (int i = 0; i < random.Next(2,10); i++) publications.Add(GetTestPublication());

            foreach (Publication i in publications) await TestRepository.AddAsync(i);

            List<Publication> results = await TestRepository.GetAllAsync();

            foreach (Publication i in publications) Assert.Contains(i, results);
        }

        [Fact]
        public async void TestUpdateAsync()
        {
            Publication publication = GetTestPublication();
            await TestRepository.AddAsync(publication);
            publication.Title = Guid.NewGuid().ToString();
            await TestRepository.UpdateAsync(publication);
            Publication result = await TestRepository.GetByIdAsync(publication.Id) ?? null!;

            Assert.Equal(publication.Title, result.Title);
        }

        private static Publication GetTestPublication()
        {
            Random random = new();

            return new Publication()
            {
                Id = Guid.NewGuid(),
                Title = Guid.NewGuid().ToString(),
                PublicationType = (PublicationType)random.Next(0, 4),
                PublishingYear = random.Next(1990, 2022)
            };
        }
    }
}