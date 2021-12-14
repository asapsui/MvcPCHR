using System;
using System.Collections.Generic;

namespace MvcPCHR.Models
{
    public partial class TestTbl
    {
        public string PatientId { get; set; } = null!;
        public string TestId { get; set; } = null!;
        public string Test { get; set; } = null!;
        public string Result { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? Note { get; set; }

        public virtual PatientTbl Patient { get; set; } = null!;
    }
}
