using System.ComponentModel.DataAnnotations;

namespace MvcPCHR.Models
{
    public class RegisterViewModel


    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public string PatientId { get; set; } = null!;

        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Range(8,15, ErrorMessage = "The password needs to be at least 8 characters long and 15 characters max")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match. Try again!")]
        //[Range(8, 15, ErrorMessage = "The password needs to be at least 8 characters long and 15 characters max")]
        public string? ConfirmPassword { get; set; }
        [Required]
        public string? LastName { get; set; } 
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }


    }
}
