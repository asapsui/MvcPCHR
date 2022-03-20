using System.ComponentModel.DataAnnotations;

namespace MvcPCHR.Models
{
    public class RegisterViewModel


    {
        [Display(Name = "Identity Number")]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public string PatientId { get; set; } = null!;

        [Required]
        public string? Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(15, ErrorMessage = "The max length is 15!!")]
        public string? Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match. Try again!")]
        [MaxLength(15, ErrorMessage = "The max length is 15!!")]
        // [Range(8, 15, ErrorMessage = "The password needs to be at least 8 characters long and 15 characters max")]
        public string? ConfirmPassword { get; set; }
        
        [Display(Name = "Last Name")]
        [Required]
        public string? LastName { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string? FirstName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }


    }
}
