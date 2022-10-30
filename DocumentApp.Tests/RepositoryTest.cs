using DocumentApp.Domain;

namespace DocumentApp.Tests
{
    public class RepositoryTest
    {
        public TestHelper MainTestHelper = new();

        [Fact]
        public async void TestAddAsync() => Assert.Equal(1, await MainTestHelper.TestRepository.AddAsync(GetTestPublication()));

        [Fact]
        public async void TestGetByIdAsync()
        {
            Publication publication = GetTestPublication();
            await MainTestHelper.TestRepository.AddAsync(publication);
            Publication? result = await MainTestHelper.TestRepository.GetByIdAsync(publication.Id) ?? null!;

            Assert.Equal(publication, result);
        }

        [Fact]
        public async void TestDeleteByIdAsync()
        {
            Publication publication = GetTestPublication();
            await MainTestHelper.TestRepository.AddAsync(publication);

            Assert.Equal(1, await MainTestHelper.TestRepository.DeleteByIdAsync(publication.Id));
        }

        [Fact] 
        public async void TestGetAllAsync()
        {
            Publication first = GetTestPublication();
            Publication second = GetTestPublication();

            await MainTestHelper.TestRepository.AddAsync(first);
            await MainTestHelper.TestRepository.AddAsync(second);
            List<Publication> publications = await MainTestHelper.TestRepository.GetAllAsync();

            Assert.True(publications.Contains(first) && publications.Contains(second));
        }

        [Fact]
        public async void TestUpdateAsync()
        {
            Publication publication = GetTestPublication();
            await MainTestHelper.TestRepository.AddAsync(publication);
            publication.Title = Guid.NewGuid().ToString();
            await MainTestHelper.TestRepository.UpdateAsync(publication);
            Publication result = await MainTestHelper.TestRepository.GetByIdAsync(publication.Id) ?? null!;

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