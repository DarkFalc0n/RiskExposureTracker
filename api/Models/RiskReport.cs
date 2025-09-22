using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiskExposureTracker.Models
{
    public class RiskReport
    {
        [Key]
        public long ReportId { get; set; }

        [Required]
        [ForeignKey("Organization")]
        public string OrgId { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public required string Period { get; set; } // e.g., Monthly, Quarterly

        [Required]
        [MaxLength(500)]
        public required string Metrics { get; set; } // JSON summary

        public DateTime CreatedAt { get; set; }

        public OrgModel? Organizations { get; set; }
    }
}
