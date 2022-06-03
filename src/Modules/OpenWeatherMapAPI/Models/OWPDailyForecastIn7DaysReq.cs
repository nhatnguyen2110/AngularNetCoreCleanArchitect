namespace OpenWeatherMapAPI.Models;

public class OWPDailyForecastIn7DaysReq
{
    public double Lon { get; set; }
    public double Lat { get; set; }
    public string? Exclude { get; set; } = "minutely";
    public string? Units { get; set; }
}
