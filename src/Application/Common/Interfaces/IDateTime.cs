namespace CleanArchitecture.Application.Common.Interfaces;

public interface IDateTime
{
    DateTime Now { get; }
    DateTime NowUtc { get; }
    DateTimeOffset ConvertStringToDateTimeOffset(string date);
    DateTimeOffset ConvertDateTimeToDateTimeOffset(DateTime? dateTime);
    DateTimeOffset ConvertDateTimeToTimeZone(DateTimeOffset dateTime, string timezoneId);
    DateTimeOffset GetUTCForEndOfCurrentDate();
    long ConvertDatetimeToUnixTimeStamp(DateTime date);
}
