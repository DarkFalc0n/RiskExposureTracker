using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_RiskExposureTrackingAndReportingSystem.Models
{
    public class Organizations
    {
        [Key]
        public long OrgId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Sector { get; set; }

        [MaxLength(50)]
        public string Region { get; set; }

        [MaxLength(100)]
        public string Contact { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public ICollection<Risks>? Risks { get; set; }
        public ICollection<ExposureSummary>? ExposureSummaries { get; set; }
        public ICollection<RiskReports>? RiskReports { get; set; }
    }
}
