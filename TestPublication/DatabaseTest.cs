using DocumentApp.Domain;

namespace TestPublication
{
    public class DatabaseTest
    {
        public TestHelper MainTestHelper = new();

        [Fact]
        public async void TestAdditionSearchDeletion()
        {
            Guid id = await TestPublicationAddition();
            await TestPublicationAcquiring(id);
            await TestPublicationDeletion(id);
        }

        public async Task<Guid> TestPublicationAddition()
        {
            Publication publication = new()
            {
                Title = "Test",
                PublicationType = PublicationType.Article,
                PublishingYear = 2022
            };

            Assert.Equal(1, await MainTestHelper.TestRepository.AddAsync(publication));
            return publication.Id;
        }

        public async Task TestPublicationAcquiring(Guid id)
        {
            Publication? result = await MainTestHelper.TestRepository.GetByIdAsync(id) ?? null!;

            Assert.Equal(id, result.Id);
            Assert.Equal("Test", result.Title);
            Assert.Equal(PublicationType.Article, result.PublicationType);
            Assert.Equal(2022, result.PublishingYear);
        }

        public async Task TestPublicationDeletion(Guid id) => Assert.Equal(1, await MainTestHelper.TestRepository.DeleteByIdAsync(id));
    }
}