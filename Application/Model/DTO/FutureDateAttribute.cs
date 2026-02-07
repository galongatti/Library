using System.ComponentModel.DataAnnotations;

namespace Library.Model.DTO;

/// <summary>
/// Validation attribute to ensure a DateTime value is in the future.
/// </summary>
public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success; // Use [Required] to check for null
        }

        if (value is DateTime dateTime)
        {
            if (dateTime.Date <= DateTime.UtcNow.Date)
            {
                return new ValidationResult(
                    ErrorMessage ?? "The date must be in the future."
                );
            }
            
            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid date format.");
    }
}

