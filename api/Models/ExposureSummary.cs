using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiskExposureTracker.Models
{
    public class ExposureSummary
    {
        [Key]
        public long SummaryId { get; set; }

        [Required]
        [ForeignKey("Organization")]
        public long OrgId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Category { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalAmt { get; set; }

        public DateTime LastUpdate { get; set; }

        public Organization? Organizations { get; set; }
    }
}
