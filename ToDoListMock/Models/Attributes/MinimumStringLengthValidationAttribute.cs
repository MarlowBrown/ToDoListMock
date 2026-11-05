using System.ComponentModel.DataAnnotations;

namespace ToDoListMock.Models.Attributes;

public class MinimumStringLengthValidationAttribute : ValidationAttribute
{
    private int length;

    public MinimumStringLengthValidationAttribute(int length = 1) => this.length = length;
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string)
        {
            string stringToCheck = (string)value;

            if (stringToCheck.Length < length)
            {
                return new ValidationResult($"string needs to be a minimum of {length} characters");
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        string valueIsStringErrorMessage = $"{validationContext.DisplayName} is not a string.";
        return new ValidationResult(valueIsStringErrorMessage);
    }
}