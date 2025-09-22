using System.ComponentModel.DataAnnotations;

namespace RiskExposureTracker.Models
{
    public class Organization
    {
        [Key]
        public long OrgId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(50)]
        public required string Sector { get; set; }

        [MaxLength(50)]
        public required Region Region { get; set; }

        [MaxLength(100)]
        public required string Contact { get; set; }

        [MaxLength(100)]
        public required string Email { get; set; }

        public ICollection<Risk>? Risks { get; set; }
        public ICollection<ExposureSummary>? ExposureSummaries { get; set; }
        public ICollection<RiskReport>? RiskReports { get; set; }
    }
}
