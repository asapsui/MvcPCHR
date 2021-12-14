using System;
using System.Collections.Generic;

namespace MvcPCHR.Models
{
    public partial class Condition
    {
        public string PatientId { get; set; } = null!;
        public string ConditionId { get; set; } = null!;
        public string Condition1 { get; set; } = null!;
        public DateTime OnsetDate { get; set; }
        public bool Acute { get; set; }
        public bool Chronic { get; set; }
        public string? Note { get; set; }

        public virtual PatientTbl Patient { get; set; } = null!;
    }
}
