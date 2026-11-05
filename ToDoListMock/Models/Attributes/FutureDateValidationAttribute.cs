using System.ComponentModel.DataAnnotations;

namespace ToDoListMock.Models.Attributes;

public class FutureDateValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime)
        {
            DateTime dateTimeValue = (DateTime)value;
            if (dateTimeValue < DateTime.Now)
            {
                return new ValidationResult("Date needs to be in the future");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
        
        
        string valueIsStringErrorMessage = $"{validationContext.DisplayName} is not a date.";
        return new ValidationResult(valueIsStringErrorMessage);
    }
}