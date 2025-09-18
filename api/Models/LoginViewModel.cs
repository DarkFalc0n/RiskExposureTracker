// LoginViewModel  

using System.ComponentModel.DataAnnotations;

namespace RiskExposureTracker.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")] // username
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]  // password
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // for React login UX
      
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
