using System;
using System.Collections.Generic;

namespace MvcPCHR.Models
{
    public partial class PrimaryCareTbl
    {
        public string PrimaryId { get; set; } = null!;
        public string? NameLast { get; set; }
        public string? NameFisrt { get; set; }
        public string? Title { get; set; }
        public string? Specialty { get; set; }
        public string? PhoneOffice { get; set; }
        public string? PhoneMobile { get; set; }
    }
}
