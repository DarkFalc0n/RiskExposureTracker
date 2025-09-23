using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RiskExposureTracker.Models
{
    public class Mitigation
    {
        [Key]
        public long MitigationId { get; set; }

        [Required]
        [ForeignKey("Risk")]
        public long RiskId { get; set; }

        [Required(ErrorMessage = "Mitigation action is required.")]
        [StringLength(200, ErrorMessage = "Action cannot exceed 200 characters.")]
        public required string Action { get; set; }

        [Required(ErrorMessage = "Owner is required.")]
        [StringLength(100, ErrorMessage = "Owner name cannot exceed 100 characters.")]
        public required string Owner { get; set; }

        [Required(ErrorMessage = "Deadline is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Mitigation), nameof(ValidateDeadline))]
        public DateTime Deadline { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(
            "Open|Completed",
            ErrorMessage = "Status must be either 'Open' or 'Completed'."
        )]
        public string Status { get; set; } = "Open";

        [JsonIgnore]
        public virtual Risk? Risk { get; set; }

        public static ValidationResult ValidateDeadline(
            DateTime deadline,
            ValidationContext context
        )
        {
            if (deadline < DateTime.Today)
            {
                return new ValidationResult("Deadline cannot be in the past.");
            }
            return ValidationResult.Success;
        }
    }
}
