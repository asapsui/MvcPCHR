using System;
using System.Collections.Generic;

namespace MvcPCHR.Models
{
    public partial class AllergyTbl
    {
        public string PatientId { get; set; } = null!;
        public string AllergyId { get; set; } = null!;
        public string Allergen { get; set; } = null!;
        public DateTime OnsetDate { get; set; }
        public string? Note { get; set; }

        public virtual PatientTbl Patient { get; set; } = null!;
    }
}
