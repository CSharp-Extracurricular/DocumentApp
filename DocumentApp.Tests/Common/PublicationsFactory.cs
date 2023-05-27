namespace DocumentApp.Tests
{
    public static class PublicationsFactory
    {
        public static Publication FirstPublication { get; set; } = null!;
        public static Publication SecondPublication { get; set; } = null!;
        public static Publication ThirdPublication { get; set; } = null!;

        public static readonly Guid FirstPublicationId = Guid.Parse("CFD1D6EE-CC12-47C2-AD05-0016FA19EA41");
        public static readonly Guid SecondPublicationId = Guid.Parse("4A4E7285-DBF4-44A8-AF86-B9ED1D16A742");
        public static readonly Guid ThirdPublicationId = Guid.Parse("582A4748-0A9F-4F89-8F68-2A2FF72BD8A9");

        static PublicationsFactory() => CreateTestData();

        public static void CreateTestData()
        {
            Publication publication1 = new()
            {
                Id = FirstPublicationId,
                Title = "Test case",
                PublicationType = PublicationType.Monography,
                PublishingYear = 1994
            };

            Publication publication3 = new()
            {
                Id = FirstPublicationId,
                Title = "Test case",
                PublicationStatus = PublicationStatus.Черновик,
                PublishingYear = 1994
            };

            Author author1_1 = new()
            {
                FirstName = "Test case 1",
                LastName = "Test case 1",
                PatronimicName = "Test case",
                Email = "example@example.com",
                Number = 0,
                PublicationId = FirstPublicationId,
                Publication = publication1
            };

            Author author1_2 = new()
            {
                FirstName = "Test case 1",
                LastName = "Test case 2",
                PatronimicName = "Test case",
                Email = "example@example.com",
                Number = 1,
                PublicationId = FirstPublicationId,
                Publication = publication1
            };

            CitationIndex index1 = new()
            {
                Indexator = Indexator.ELibrary,
                URL = new Uri("https://learn.microsoft.com/ru-ru/dotnet/api/system.uri?view=net-7.0"),
                PublicationId = FirstPublicationId,
                Publication = publication1
            };

            Conference conference1 = new()
            {
                ShortName = "Test case",
                FullName = "Test case",
                StartDate = new DateTime(2022, 12, 12),
                EndDate = new DateTime(2022, 12, 12),
                Type = ConferenceType.International,
                Location = "Test case",
                PublicationId = FirstPublicationId,
                Publication = publication1
            };

            publication1.Authors.Add(author1_1);
            publication1.Authors.Add(author1_2);
            publication1.CitationIndices.Add(index1);
            publication1.Conference = conference1;

            Publication publication2 = new()
            {
                Id = SecondPublicationId,
                Title = "Test case 1",
                PublicationType = PublicationType.Monography,
                PublishingYear = 1994
            };

            Publication publication4 = new()
            {
                Id = FirstPublicationId,
                Title = "Test case",
                PublicationStatus = PublicationStatus.Черновик,
                PublishingYear = 1994
            };

            Author author2_1 = new()
            {
                FirstName = "Test case 2",
                LastName = "Test case 1",
                PatronimicName = "Test case",
                Email = "example@example.com",
                Number = 0,
                PublicationId = SecondPublicationId,
                Publication = publication2
            };

            Author author2_2 = new()
            {
                FirstName = "Test case 2",
                LastName = "Test case 2",
                PatronimicName = "Test case",
                Email = "example@example.com",
                Number = 1,
                PublicationId = SecondPublicationId,
                Publication = publication2
            };

            CitationIndex index2 = new()
            {
                Indexator = Indexator.ELibrary,
                URL = new Uri("https://learn.microsoft.com/ru-ru/dotnet/api/system.uri?view=net-7.0"),
                PublicationId = SecondPublicationId,
                Publication = publication2
            };

            Conference conference2 = new()
            {
                ShortName = "Test case",
                FullName = "Test case",
                StartDate = new DateTime(2022, 12, 12),
                EndDate = new DateTime(2022, 12, 12),
                Type = ConferenceType.International,
                Location = "Test case",
                PublicationId = SecondPublicationId,
                Publication = publication2
            };

            publication2.Authors.Add(author2_1);
            publication2.Authors.Add(author2_2);
            publication2.CitationIndices.Add(index2);
            publication2.Conference = conference2;

            Publication publication3 = new()
            {
                Id = ThirdPublicationId,
                Title = "Test case 2",
                PublicationType = PublicationType.Textbook,
                PublishingYear = 2003
            };

            Author author3_1 = new()
            {
                FirstName = "Test case 3",
                LastName = "Test case 1",
                PatronimicName = "Test case",
                Email = "example@example.com",
                Number = 0,
                PublicationId = ThirdPublicationId,
                Publication = publication3
            };

            Author author3_2 = new()
            {
                FirstName = "Test case 3",
                LastName = "Test case 2",
                PatronimicName = "Test case",
                Email = "example@example.com",
                Number = 1,
                PublicationId = ThirdPublicationId,
                Publication = publication3
            };

            CitationIndex index3 = new()
            {
                Indexator = Indexator.ELibrary,
                URL = new Uri("https://learn.microsoft.com/ru-ru/dotnet/api/system.uri?view=net-7.0"),
                PublicationId = ThirdPublicationId,
                Publication = publication3
            };

            Conference conference3 = new()
            {
                ShortName = "Test case",
                FullName = "Test case",
                StartDate = new DateTime(2022, 12, 12),
                EndDate = new DateTime(2022, 12, 12),
                Type = ConferenceType.International,
                Location = "Test case",
                PublicationId = ThirdPublicationId,
                Publication = publication3
            };

            publication3.Authors.Add(author3_1);
            publication3.Authors.Add(author3_2);
            publication3.CitationIndices.Add(index3);
            publication3.Conference = conference3;

            FirstPublication = publication1;
            SecondPublication = publication2;
            ThirdPublication = publication3;
        }
    }
}