using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiskExposureTracker.Models
{
    public class Risk
    {
        [Key]
        public long RiskId { get; set; }

        [ForeignKey(nameof(Organizations))]
        public string OrgId { get; set; } = string.Empty;

        [Required]
        public required RiskCategory Category { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Description { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Exposure { get; set; }

        [Required]
        public required RiskStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public OrgModel? Organizations { get; set; }

        public ICollection<Mitigation>? Mitigations { get; set; }
    }
}
