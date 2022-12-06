namespace DocumentApp.Domain
{
    public class Author : IIdentifiableT
    {
        public Guid Id { get; set; }

        // Идентификатор автора в системе верхнего уровня.
        public Guid ObjectId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PatronimicName { get; set; }
        public string Email { get; set; } = string.Empty;
        
        // Номер автора в списке авторов публикации. 
        public int Number { get; set; }

        public Guid PublicationId { get; set; }

        public Publication Publication { get; set; } = null!;
    }
}
