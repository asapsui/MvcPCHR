using System;
using System.Collections.Generic;

namespace MvcPCHR.Models
{
    public partial class MedicationTbl
    {
        public string PatientId { get; set; } = null!;
        public string MedId { get; set; } = null!;
        public string Medication { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool Chronic { get; set; }
        public string? Note { get; set; }

        public virtual PatientTbl Patient { get; set; } = null!;
    }
}
