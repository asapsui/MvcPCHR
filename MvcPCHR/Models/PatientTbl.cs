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

        [Display(Name = "Identity Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public string PatientId { get; set; } = null!;
        
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;
        
        [Required]
        //[DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        // DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")] 
        public DateTime DateOfBirth { get; set; }
        
        [Display(Name = "Street Address")]
        public string? AddressStreet { get; set; }

        [Display(Name = "City")]
        public string? AddressCity { get; set; }

        [Display(Name = "State")]
        public string? AddressState { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        [MaxLength(5, ErrorMessage = "The max length is 15!!")]
        [Display(Name = "Zip Code")]
        public string? AddressZip { get; set; }

        [Display(Name = "Home Telephone")]
        [DisplayFormat(DataFormatString = "{0:###-###-####}")] // this may not be working correctly
        public string? PhoneHome { get; set; }

        [Display(Name = "Mobile Telephone")]
        [DisplayFormat(DataFormatString = "{0:###-###-####}")]
        public string? PhoneMobile { get; set; }
        public string? PrimaryId { get; set; }
        [Required]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessage = "The max length is 15!!")]
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
