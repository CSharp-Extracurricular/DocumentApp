using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentApp.Domain
{
    public enum Indexator
    {
        RSCI,
        ELibrary,
        Scopus,
        WebOfScience
    }

    public class CitationIndex
    {
        public Guid Id { get; set; }
        public Indexator Indexator { get; set; }
        public string URL { get; set; } = string.Empty;
    }
}
