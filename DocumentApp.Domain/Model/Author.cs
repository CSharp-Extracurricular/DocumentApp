using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
