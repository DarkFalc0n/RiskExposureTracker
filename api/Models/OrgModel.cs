using Microsoft.AspNetCore.Identity;

namespace RiskExposureTracker.Models
{
    public class OrgModel : IdentityUser
    {
        public required string Name { get; set; }
        public required string Sector { get; set; }
        public required Region Region { get; set; }
        public required string Contact { get; set; }
        public string? Role { get; set; }
    }
}
