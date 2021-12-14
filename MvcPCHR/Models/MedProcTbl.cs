using System;
using System.Collections.Generic;

namespace MvcPCHR.Models
{
    public partial class MedProcTbl
    {
        public string PatientId { get; set; } = null!;
        public string ProcedureId { get; set; } = null!;
        public string MedProcedure { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Doctor { get; set; } = null!;
        public string? Note { get; set; }

        public virtual PatientTbl Patient { get; set; } = null!;
    }
}
