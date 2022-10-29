using DocumentApp.Domain;
using DocumentApp.Infrastructure;

namespace TestPublication
{
    public class DatabaseTest
    {
        public TestHelper MainTestHelper = new();
        
        [Fact]
        public async void TestAdditionAndSearch()
        {
            Guid testGuid = await TestPublicationAddition();
            await TestPublicationAcquiring(testGuid);
        }

        
        public async Task<Guid> TestPublicationAddition()
        {
            PublicationRepository MainTestRepository = MainTestHelper.TestRepository;
            Publication publication = new()
            {
                Title = "Test",
                PublicationType = PublicationType.Article,
                PublishingYear = 2022
            };

            Assert.Equal(1, await MainTestRepository.AddAsync(publication));
            return publication.Id;


        }
        
        public async Task TestPublicationAcquiring(Guid testGuid)
        {
            PublicationRepository MainTestRepository = MainTestHelper.TestRepository;
            Publication? result = await MainTestRepository.GetByIdAsync(testGuid) ?? null!;

            Assert.Equal(testGuid, result.Id);
            Assert.Equal("Test", result.Title);
            Assert.Equal(PublicationType.Article, result.PublicationType);
            Assert.Equal(2022, result.PublishingYear);
        }
    }
}