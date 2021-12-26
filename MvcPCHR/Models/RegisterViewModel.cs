using System.ComponentModel.DataAnnotations;

namespace MvcPCHR.Models
{
    public class RegisterViewModel


    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public string PatientId { get; set; } = null!;

        [Required]
        public string? Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        //[Range(8, 15, ErrorMessage = "The password needs to be at least 8 characters long and 15 characters max")]
        // this range is not working 

        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match. Try again!")]
        // [Range(8, 15, ErrorMessage = "The password needs to be at least 8 characters long and 15 characters max")]
        public string? ConfirmPassword { get; set; }
        [Required]
        public string? LastName { get; set; } 
        [Required]
        public string? FirstName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }


    }
}
