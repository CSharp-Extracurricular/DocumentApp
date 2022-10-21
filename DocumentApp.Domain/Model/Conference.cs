using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentApp.Domain
{
    public enum ConferenceType
    {
        Local,
        Regional,
        National,
        International
    }

    public class Conference
    {
        public Guid Id { get; set; }
        public string ShortName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ConferenceType Type { get; set; }    

        public Guid PublicationId { get; set; }

        public Publication Publication { get; set; } = null!;
    }
}