using System.Globalization;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
    public DateTime NowUtc => DateTime.UtcNow;
    public DateTimeOffset ConvertStringToDateTimeOffset(string date)
    {
        if (!string.IsNullOrEmpty(date) && date.Contains("-"))
            date = date.Replace("-", "/");
        var result = (date == "0000-00-00" || date == null) ? DateTimeOffset.Now : DateTimeOffset.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        return result;
    }
    public DateTimeOffset ConvertDateTimeToDateTimeOffset(DateTime? dateTime)
    {
        if (dateTime != null)
            return new DateTimeOffset(dateTime ?? NowUtc);

        return DateTimeOffset.Now;
    }

    public DateTimeOffset ConvertDateTimeToTimeZone(DateTimeOffset dateTime, string timezoneId)
    {
        DateTimeOffset utcTime = dateTime.ToUniversalTime();
        //TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
        TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
        DateTimeOffset userTime = TimeZoneInfo.ConvertTime(utcTime, timeInfo);
        var result = DateTimeOffset.Parse(userTime.ToString("yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture));
        return result;
    }

    public DateTimeOffset GetUTCForEndOfCurrentDate()
    {
        return new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59, DateTimeOffset.Now.Offset).ToUniversalTime();
    }

    public long ConvertDatetimeToUnixTimeStamp(DateTime date)
    {
        var dateTimeOffset = new DateTimeOffset(date);
        var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();
        return unixDateTime;
    }
}
