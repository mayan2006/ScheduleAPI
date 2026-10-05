using System.ComponentModel.DataAnnotations;

namespace Schedule.Core.Validation
{
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        public DateNotInFutureAttribute()
        {
            ErrorMessage = "Date cannot be in the future.";
        }

        public override bool IsValid(object? value)
        {
            if (value is not DateTime date)
                return true;

            return date.Date <= DateTime.UtcNow.Date;
        }
    }
}
