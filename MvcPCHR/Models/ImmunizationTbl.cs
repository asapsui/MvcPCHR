using System;
using System.Collections.Generic;

namespace MvcPCHR.Models
{
    public partial class ImmunizationTbl
    {
        public string PatientId { get; set; } = null!;
        public string ImmunizationId { get; set; } = null!;
        public string Immunization { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? Note { get; set; }

        public virtual PatientTbl Patient { get; set; } = null!;
    }
}
