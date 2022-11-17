using DocumentApp.Infrastructure;
using DocumentApp.Domain;
using System;

namespace DocumentApp.Tests
{
    public class RepositoryTest
    {
        public readonly TestHelper MainTestHelper = new();

        private PublicationRepository TestRepository => MainTestHelper.TestRepository;

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

        //[Fact]
        //public async void TestUpdateAddAsync()
        //{
        //    //Publication publication2 = GetTestPublication();
        //    //_ = TestRepository.AddAsync(publication2).Result;
        //    //publication.Title = Guid.NewGuid().ToString();



        //    //Publication publication = await TestRepository.GetByIdAsync(publication2.Id)
        //    //    ?? throw new NullReferenceException("Publication not found");
        //    //publication.Title = "Test TestUpdateAddAsync";


        //    ////for (int i = 0; i < 1; i++)
        //    ////{
        //    ////    var author = GetTestAuthor();
        //    ////    author.Number = 2;
        //    ////    publication.Authors.Add(author);
        //    ////    //publication.CitationIndices.Add(GetTestCitationIndex());
        //    ////}

        //    var author = GetTestAuthor();
        //    author.Number = 2;
        //    publication.Authors.Add(author);

        //    //publication.Conference = GetTestConference();

        //    await TestRepository.UpdateAsync(publication);

        //    Publication? result = await TestRepository.GetByIdAsync(publication.Id) ?? null!;
        //    ComparePublications(publication, result);
        //}

        [Fact]
        public async void TestUpdateAddAsync()
        {
            var testGuid = "10000000-0000-0000-0000-000000000000";
            var testGuid2 = "20000000-0000-0000-0000-000000000000";
            Publication publication = new Publication
            {
                Id = new Guid(testGuid),
                Title = "Test title",
                PublicationType = PublicationType.Article
            };

            TestRepository.AddAsync(publication).Wait();

            var author = new Author
            {
                Id = new Guid(testGuid2),
                Number = 1,
                FirstName = "Test",
                LastName = "Test"
            };

            var publication2 = new Publication
            {
                Id = new Guid(testGuid),
                Title = "2 Test title",
                PublicationType = PublicationType.Article
            };
            
            publication2.Authors.Add(author);

            TestRepository.UpdateAsync(publication2).Wait();

            Publication? result = TestRepository.GetByIdAsync(publication2.Id).Result ?? null!;
            //ComparePublications(publication2, result);
            Assert.Single(result.Authors);

        }

        private static void ComparePublications(Publication publication, Publication result)
        {
            //Assert.Equal(publication, result);
            Assert.Equal(publication.Authors, result.Authors);
            //Assert.Equal(publication.CitationIndices, result.CitationIndices);
            //Assert.Equal(publication.Conference, result.Conference);
        }

        //static Random random = new();

        private static Publication GetTestPublication()
        {
            Publication publication = new()
            {
                Id = Guid.NewGuid(),
                Title = Guid.NewGuid().ToString(),
                //PublicationType = (PublicationType)random.Next(0, 4),
                PublicationType = PublicationType.Article,
                //PublishingYear = random.Next(1990, 2022)
                PublishingYear = 1990
            };

            int cnt = 1;
            for (int i = 0; i < cnt; i++)
            {
                publication.Authors.Add(GetTestAuthor());
                //publication.CitationIndices.Add(GetTestCitationIndex());
            }

            //publication.Conference = GetTestConference();

            return publication;
        }

        private static Author GetTestAuthor()
        {
            return new Author()
            {
                Id = Guid.NewGuid(),

                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                PatronimicName = Guid.NewGuid().ToString(),
                Number = 1
            };
        }

        private static Conference GetTestConference()
        {
            return new Conference()
            {
                Id = Guid.NewGuid(),
                ShortName = Guid.NewGuid().ToString(),
                FullName = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Type = (ConferenceType.International),
                Location = Guid.NewGuid().ToString()
            };
        }

        private static CitationIndex GetTestCitationIndex()
        {
            return new CitationIndex()
            {
                Id = Guid.NewGuid(),
                Indexator = (Indexator.ELibrary),
                URL = Guid.NewGuid().ToString()
            };
        }
    }
}