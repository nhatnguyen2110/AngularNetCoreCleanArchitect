namespace OpenWeatherMapAPI.Models;

public class OWPHistoricalWeatherReq
{
    public double Dt { get; set; }
    public double Lon { get; set; }
    public double Lat { get; set; }
    public string? Units { get; set; } = "metric";
}
