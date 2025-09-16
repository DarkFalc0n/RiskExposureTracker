using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject_RiskExposureTrackingAndReportingSystem.Models
{
    public class Risks
    {
        [Key]
        public long RiskId { get; set; }

        [Required]
        [ForeignKey("Organization")]
        public long OrgId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Category { get; set; }  

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Exposure { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }  

        public DateTime CreatedAt { get; set; }

        public Organizations? Organizations { get; set; }

        public ICollection<Mitigations>? Mitigations { get; set; }
    }
}
