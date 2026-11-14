using System.ComponentModel.DataAnnotations;

namespace Mu.Api.Infrastructure.Validation;

public class MuFutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        if (value is not DateTime date)
        {
            throw new InvalidOperationException("MuFutureDateAttribute can only be used on DateTime type");
        }

        if (date.Kind is DateTimeKind.Unspecified)
        {
            return new ValidationResult("Date must include a time zone (e.g. Z or +01:00)");
        }

        var dateUtc = date.ToUniversalTime();
        var currentDateUtc = DateTime.UtcNow;

        return dateUtc > currentDateUtc ? ValidationResult.Success : new ValidationResult("Date must be in the future");

    }
}