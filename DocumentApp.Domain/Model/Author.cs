using System.Text.Json.Serialization;

namespace DocumentApp.Domain
{
    public class Author : IIdentifiableT
    {
        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PatronimicName { get; set; }
        public int Number { get; set; }

        public Guid PublicationId { get; set; }

        [JsonIgnore] public Publication Publication { get; set; } = null!;
    }
}
