using System.ComponentModel.DataAnnotations;

namespace BackgroundJobUI.Extensions;

[AttributeUsage(AttributeTargets.Property)]
public class RunningDateAttribute : ValidationAttribute
{
    public RunningDateAttribute(string errorMessage) : base(errorMessage) { }

    public override bool IsValid(object value)
    {
        if (value == null) return false;

        DateTime? dateTime = value as DateTime?;

        if (dateTime.HasValue)
        {
            return dateTime.Value >= DateTime.Now.AddDays(-5);
        }

        return false;
    }
}