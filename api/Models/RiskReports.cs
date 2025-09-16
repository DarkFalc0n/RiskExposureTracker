using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject_RiskExposureTrackingAndReportingSystem.Models
{
    public class RiskReports
    {
        [Key]
        public long ReportId { get; set; }

        [Required]
        [ForeignKey("Organization")]
        public long OrgId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Period { get; set; }  // e.g., Monthly, Quarterly

        [Required]
        [MaxLength(500)]
        public string Metrics { get; set; }  // JSON summary

        public DateTime CreatedAt { get; set; }

        public Organizations? Organizations { get; set; }
    }
}
