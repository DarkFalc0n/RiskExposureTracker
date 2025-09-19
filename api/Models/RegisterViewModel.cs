// things added here in RegisterViewModel.cs => uesrname , password , confirm password

using System.ComponentModel.DataAnnotations;

namespace RiskExposureTracker.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")] // username
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")] // paswrd
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")] // confirm paswrd
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        // user role selection  (if needed)
        // public string? Role { get; set; }
    }
}
