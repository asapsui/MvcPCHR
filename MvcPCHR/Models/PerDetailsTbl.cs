using System;
using System.Collections.Generic;

namespace MvcPCHR.Models
{
    public partial class PerDetailsTbl
    {
        public string PatientId { get; set; } = null!;
        public string? BloodType { get; set; }
        public bool? OrganDonor { get; set; }
        public bool? HivStatus { get; set; }
        public short? HeightInches { get; set; }
        public short? WeightLbs { get; set; }

        public virtual PatientTbl Patient { get; set; } = null!;
    }
}
