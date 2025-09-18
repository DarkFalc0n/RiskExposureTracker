using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RiskExposureTracker.Models
{
    public class Mitigation
    {
        [Key]
        public long MitigationId { get; set; } // primary key

        // --------------------------------------------------------------------------

        [Required]
        [ForeignKey("Risk")]
        public long RiskId { get; set; } // frgn key

        // --------------------------------------------------------------------------

        [Required(ErrorMessage = "Mitigation action is required.")]
        [StringLength(200, ErrorMessage = "Action cannot exceed 200 characters.")]
        public required string Action { get; set; } // Mitigation step description


        // --------------------------------------------------------------------------

        [Required(ErrorMessage = "Owner is required.")]
        [StringLength(100, ErrorMessage = "Owner name cannot exceed 100 characters.")]
        public required string Owner { get; set; } // owner -> person responsible

        // --------------------------------------------------------------------------


        [Required(ErrorMessage = "Deadline is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Mitigation), nameof(ValidateDeadline))]
        public DateTime Deadline { get; set; } // target completion date

        // --------------------------------------------------------------------------

        [Required]
        [StringLength(20)]
        [RegularExpression("Open|Completed", ErrorMessage = "Status must be either 'Open' or 'Completed'.")]
        public string Status { get; set; } = "Open"; // Default value


        // --------------------------------------------------------------------------

        // Navigation property
        public virtual required Risk Risk { get; set; } // Risks model from project part 1

        // Custom deadline validation
        public static ValidationResult ValidateDeadline(DateTime deadline, ValidationContext context)
        {
            if (deadline < DateTime.Today)
            {
                return new ValidationResult("Deadline cannot be in the past.");
            }
            return ValidationResult.Success;
        }
    }
}
