using System.ComponentModel.DataAnnotations;

namespace ToDoListMock.Models.Attributes;

public class MinimumIntegerValidationAttribute : ValidationAttribute
{
    private int minimumNumber;

    
    public MinimumIntegerValidationAttribute(int minimumNumber = 1) => this.minimumNumber = minimumNumber;
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is int)
        {
            int intValue = (int)value;
            if (intValue < minimumNumber)
            {
                return new ValidationResult($"Number needs to be greater than or equal to {minimumNumber}");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
        
        
        string valueIsStringErrorMessage = $"{validationContext.DisplayName} is not an integer.";
        return new ValidationResult(valueIsStringErrorMessage);
    }
}