using System.ComponentModel.DataAnnotations;

namespace RiskExposureTracker.Models
{
    public class RegisterRequest
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Sector { get; set; }

        [Required]
        public required Region Region { get; set; }

        [Required]
        public required string Contact { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(8)]
        public required string Password { get; set; }
    }
}
