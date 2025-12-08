using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Models
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateTimeValue)
            {
                if (dateTimeValue > DateTime.Now)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage ?? "The date must be in the future.");
                }
            }
            return new ValidationResult(ErrorMessage ?? "Invalid date format.");
        }
    }
}





