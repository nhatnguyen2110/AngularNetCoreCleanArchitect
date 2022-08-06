using System.ComponentModel.DataAnnotations;

namespace BackgroundJobUI.Extensions;

[AttributeUsage(AttributeTargets.Property)]
public class RunningDateAttribute : ValidationAttribute
{
    public RunningDateAttribute(string errorMessage) : base(errorMessage) { }

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool IsValid(object value)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
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