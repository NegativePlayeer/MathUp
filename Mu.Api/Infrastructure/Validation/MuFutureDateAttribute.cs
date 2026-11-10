using System.ComponentModel.DataAnnotations;

namespace Mu.Api.Infrastructure.Validation;

public class MuFutureDateAttribute : ValidationAttribute
{
    public MuFutureDateAttribute()
    {
        ErrorMessage = "Date must be in the future";
    }

    public override bool IsValid(object? value)
    {
        if (value is null) return true;
        if (value is DateTime date)
        {
            var currentDate = DateTime.Now;
            return date > currentDate;
        }
        return false;
    }
}