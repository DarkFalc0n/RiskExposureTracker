using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiskExposureTracker.Models
{
    public class Risk
    {
        [Key]
        public long RiskId { get; set; }

        [Required]
        [ForeignKey("Organization")]
        public long OrgId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Category { get; set; }  

        [Required]
        [MaxLength(200)]
        public required string Description { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Exposure { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Status { get; set; }  

        public DateTime CreatedAt { get; set; }

        public Organization? Organizations { get; set; }

        public ICollection<Mitigation>? Mitigations { get; set; }
    }
}
