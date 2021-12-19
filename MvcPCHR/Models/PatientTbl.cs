using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcPCHR.Models
{
    public partial class PatientTbl
    {
        public PatientTbl()
        {
            AllergyTbls = new HashSet<AllergyTbl>();
            Conditions = new HashSet<Condition>();
            ImmunizationTbls = new HashSet<ImmunizationTbl>();
            MedProcTbls = new HashSet<MedProcTbl>();
            MedicationTbls = new HashSet<MedicationTbl>();
            TestTbls = new HashSet<TestTbl>();
        }

        public string PatientId { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        [Required]
        public DateTime? DateOfBirth { get; set; }
        public string? AddressStreet { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressState { get; set; }
        public string? AddressZip { get; set; }
        public string? PhoneHome { get; set; }
        public string? PhoneMobile { get; set; }
        public string? PrimaryId { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }

        public virtual PerDetailsTbl PerDetailsTbl { get; set; } = null!;
        public virtual ICollection<AllergyTbl> AllergyTbls { get; set; }
        public virtual ICollection<Condition> Conditions { get; set; }
        public virtual ICollection<ImmunizationTbl> ImmunizationTbls { get; set; }
        public virtual ICollection<MedProcTbl> MedProcTbls { get; set; }
        public virtual ICollection<MedicationTbl> MedicationTbls { get; set; }
        public virtual ICollection<TestTbl> TestTbls { get; set; }
    }
}
