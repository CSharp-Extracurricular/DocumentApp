using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentApp.Domain
{
    public enum PublicationType
    {
        Monography,
        Textbook,
        Article,
        Report,
        Thesis
    }

    public class Publication
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int PublishingYear { get; set; }
        public PublicationType PublicationType { get; set; }

        public List<Author> Authors { get; set; } = new List<Author>();
        public List<CitationIndex> Indices { get; set; } = new List<CitationIndex>();
        public Conference? Conference { get; set; } 
        public HashSet<string> KeyWords { get; set; } = new HashSet<string>();
    }
}
