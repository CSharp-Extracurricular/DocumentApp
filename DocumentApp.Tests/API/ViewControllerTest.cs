using DocumentApp.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DocumentApp.Tests
{
    [Collection("MainTestCollection")]
    public class ViewControllerTest
    {
        private PublicationRepository TestRepository { get; }
        private ViewController ViewController { get; }

        public ViewControllerTest()
        {
            TestHelper MainTestHelper = new();
            TestRepository = MainTestHelper.TestRepository;
            ViewController = new ViewController(MainTestHelper.TestRepository.UnitOfWork);
        }

        [Fact]
        public async Task GetPublicationTestAsync()
        {
            Publication publication = PublicationsFactory.FirstPublication;
            await TestRepository.AddAsync(publication);

            ActionResult<PublicationDto> responseResult = await ViewController.GetPublicationAsync(publication.Id);
            
            Assert.IsType<OkObjectResult>(responseResult.Result);
            OkObjectResult? okObjectResult = responseResult.Result as OkObjectResult;
            PublicationDto? responseResultValue = okObjectResult.Value as PublicationDto;

            Publication resultPublication = DtoConverter.ConvertToNative(responseResultValue);
            Assert.Equal(publication, resultPublication, new PublicationsEqualityComparer());
        }

        [Fact]
        public async Task GetAllPublicationsTestAsync()
        {
            Publication first = PublicationsFactory.FirstPublication;
            Publication second = PublicationsFactory.SecondPublication;
            await TestRepository.AddAsync(first);
            await TestRepository.AddAsync(second);

            ActionResult<IEnumerable<PublicationDto>> responseResult = await ViewController.GetAllPublicationsAsync();

            Assert.IsType<OkObjectResult>(responseResult.Result);
            OkObjectResult? okObjectResult = responseResult.Result as OkObjectResult;
            IEnumerable<PublicationDto>? responseResultValue = okObjectResult.Value as IEnumerable<PublicationDto>;

            foreach(PublicationDto publicationDto in responseResultValue)
            {
                Publication resultPublication = DtoConverter.ConvertToNative(publicationDto);
                Assert.Equal(resultPublication, resultPublication, new PublicationsEqualityComparer());
            }
        }

        [Fact]
        public async Task GetFilteredByStartYearPublicationsTestAsync()
        {
            Publication first = PublicationsFactory.FirstPublication;
            Publication third = PublicationsFactory.ThirdPublication;
            await TestRepository.AddAsync(first);
            await TestRepository.AddAsync(third);

            PublicationQuery query = new PublicationQuery()
            {
                StartYear = 2003
            };

            ActionResult<IEnumerable<PublicationDto>> responseResult = await ViewController.GetFilteredPublicationsAsync(query);

            Assert.IsType<OkObjectResult>(responseResult.Result);
            OkObjectResult? okObjectResult = responseResult.Result as OkObjectResult;
            IEnumerable<PublicationDto>? responseResultValue = okObjectResult.Value as IEnumerable<PublicationDto>;

            Assert.Equal(1, responseResultValue.Count());
            PublicationDto thirdDto = DtoConverter.Convert(third);
            Assert.Equal(thirdDto, responseResultValue.First(), new PublicationsDtoEqualityComparer());
        }

        [Fact]
        public async Task GetFilteredByEndYearPublicationsTestAsync()
        {
            Publication first = PublicationsFactory.FirstPublication;
            Publication second = PublicationsFactory.SecondPublication;
            Publication third = PublicationsFactory.ThirdPublication;
            await TestRepository.AddAsync(first);
            await TestRepository.AddAsync(second);
            await TestRepository.AddAsync(third);

            PublicationQuery query = new PublicationQuery()
            {
                EndYear = 1994
            };

            ActionResult<IEnumerable<PublicationDto>> responseResult = await ViewController.GetFilteredPublicationsAsync(query);

            Assert.IsType<OkObjectResult>(responseResult.Result);
            OkObjectResult? okObjectResult = responseResult.Result as OkObjectResult;
            IEnumerable<PublicationDto>? responseResultValue = okObjectResult.Value as IEnumerable<PublicationDto>;

            Assert.Equal(2, responseResultValue.Count());
            PublicationDto firstDto = DtoConverter.Convert(first);
            PublicationDto secondDto = DtoConverter.Convert(second);
            Assert.Contains(firstDto, responseResultValue, new PublicationsDtoEqualityComparer());
            Assert.Contains(secondDto, responseResultValue, new PublicationsDtoEqualityComparer());
        }

        [Fact]
        public async Task GetFilteredByTypePublicationsTestAsync()
        {
            Publication first = PublicationsFactory.FirstPublication;
            Publication third = PublicationsFactory.ThirdPublication;
            await TestRepository.AddAsync(first);
            await TestRepository.AddAsync(third);

            PublicationQuery query = new PublicationQuery()
            {
                PublicationType = PublicationType.Textbook
            };

            ActionResult<IEnumerable<PublicationDto>> responseResult = await ViewController.GetFilteredPublicationsAsync(query);

            Assert.IsType<OkObjectResult>(responseResult.Result);
            OkObjectResult? okObjectResult = responseResult.Result as OkObjectResult;
            IEnumerable<PublicationDto>? responseResultValue = okObjectResult.Value as IEnumerable<PublicationDto>;

            Assert.Equal(1, responseResultValue.Count());
            PublicationDto thirdDto = DtoConverter.Convert(third);
            Assert.Equal(thirdDto, responseResultValue.First(), new PublicationsDtoEqualityComparer());
        }
    }
}