using DocumentApp.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DocumentApp.Tests
{
    [Collection("MainTestCollection")]
    public class EditControllerTest
    {
        private PublicationRepository TestRepository { get; }
        private EditController EditController { get; }

        public EditControllerTest()
        {
            TestHelper mainTestHelper = new();
            TestRepository = mainTestHelper.TestRepository;
            EditController = new EditController(mainTestHelper.TestRepository.UnitOfWork);
        }

        [Fact]
        public void CreateEditControllerTest()
        {
            EditController controller = new(TestRepository.UnitOfWork);
            Assert.NotNull(controller);
        }

        [Fact]
        public void CreateEditControllerWithoutContextTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new EditController(null!);
            });
        }

        [Fact]
        public async Task PutPublicationTestAsync()
        {
            Publication publication = PublicationsFactory.FirstPublication;
            await TestRepository.AddAsync(publication);

            publication.Title = "Changed.";
            PublicationDto publicationDto = DtoConverter.Convert(publication);
            IActionResult responseResult = await EditController.PutPublication(publicationDto);

            Assert.IsType<NoContentResult>(responseResult);
            Assert.Equal(publication, await TestRepository.GetByIdAsync(publication.Id) ?? null!, new PublicationsEqualityComparer());
        }

        [Fact]
        public async Task PostPublicationsTestAsync()
        {
            Publication publication = PublicationsFactory.FirstPublication;
            PublicationDto publicationDto = DtoConverter.Convert(publication);

            ActionResult<PublicationDto> responseResult = await EditController.PostPublications(publicationDto);

            CreatedResult? okObjectResult = responseResult.Result as CreatedResult;
            PublicationDto responseResultValue = okObjectResult.Value as PublicationDto ?? null!;

            Assert.Equal(publicationDto, responseResultValue, new PublicationsDtoEqualityComparer());
        }

        [Fact]
        public async Task DeletePublicationTestAsync()
        {
            Publication publication = PublicationsFactory.FirstPublication;
            await TestRepository.AddAsync(publication);

            IActionResult responseResult = await EditController.DeletePublication(publication.Id);

            Assert.IsType<NoContentResult>(responseResult);
            Assert.Null(await TestRepository.GetByIdAsync(publication.Id));
        }
    }
}