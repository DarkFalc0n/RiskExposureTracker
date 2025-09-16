using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject_RiskExposureTrackingAndReportingSystem.Models
{
    public class Mitigations
    {
        [Key]
        public long MitigationId { get; set; }

        [Required]
        [ForeignKey("Risk")]
        public long RiskId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Action { get; set; }

        [Required]
        [MaxLength(100)]
        public string Owner { get; set; }

        public DateTime Deadline { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }  // e.g., Open, Completed

        public Risks? Risk { get; set; }

    }
}
