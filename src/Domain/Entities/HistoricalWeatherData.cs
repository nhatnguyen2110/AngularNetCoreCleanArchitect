namespace CleanArchitecture.Domain.Entities;

public class HistoricalWeatherData : AuditableEntity
{
    public int Id { get; set; }
    public int ProvinceId { get; set; }
    public double Dt { get; set; }
    public double Sunrise { get; set; }
    public double Sunset { get; set; }
    public double Temp_avg { get; set; }
    public double Temp_min { get; set; }
    public double Temp_max { get; set; }
    public double Humidity { get; set; }
    public double Dew_point { get; set; }
    public double Pop { get; set; }
    public double Wind_speed { get; set; }
    public double Clouds { get; set; }
    public int Weather_id { get; set; }
    public string? Weather_main { get; set; }
    public string? Weather_description { get; set; }
    public string? Weather_icon { get; set; }
    public double Temp_morn { get; set; }
    public double Temp_day { get; set; }
    public double Temp_eve { get; set; }
    public double Temp_night { get; set; }
    public int WeatherId_morn { get; set; }
    public string? WeatherMain_morn { get; set; }
    public string? WeatherDesc_morn { get; set; }
    public string? WeatherIcon_morn { get; set; }
    public int WeatherId_day { get; set; }
    public string? WeatherMain_day { get; set; }
    public string? WeatherDesc_day { get; set; }
    public string? WeatherIcon_day { get; set; }
    public int WeatherId_eve { get; set; }
    public string? WeatherMain_eve { get; set; }
    public string? WeatherDesc_eve { get; set; }
    public string? WeatherIcon_eve { get; set; }
    public int WeatherId_night { get; set; }
    public string? WeatherMain_night { get; set; }
    public string? WeatherDesc_night { get; set; }
    public string? WeatherIcon_night { get; set; }

    public Province Province { get; set; } = null!;
}
