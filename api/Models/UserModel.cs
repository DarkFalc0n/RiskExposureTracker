namespace RiskExposureTracker.Models
{
    public class UserModel
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? Role { get; set; }
    }
}
