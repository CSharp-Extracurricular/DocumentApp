namespace DocumentApp.Domain
{
    public class Author
    {
        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PatronimicName { get; set; }
        public int Number { get; set; }

        public Guid PublicationId { get; set; }
        public Publication Publication { get; set; } = null!;
    }
}
