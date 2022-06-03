using CleanArchitecture.Domain.ApiClient;

namespace OpenWeatherMapAPI.Models;

#region Api Response
public class OWPOneCallRes : ApiResponse
{
    public double lat { get; set; }
    public double lon { get; set; }
    public string? timezone { get;set; }
    public double timezone_offset { get; set; }
    public OWPCurrentForecastWeather? current { get; set; }
    public List<OWPHourlyForecastWeather> hourly { get; set; } = new List<OWPHourlyForecastWeather>();
    public List<OWPDailyForecastWeather> daily { get; set; } = new List<OWPDailyForecastWeather>();
}
public class OWPWeatherForecastRes : ApiResponse
{
    public double id { get; set; }
    public double dt { get; set; }
    public double timezone { get; set; }
    public string? name { get; set; }
    public double cod { get; set; }
    public OWPCoord? coord { get; set; }
    public OWPWeatherInfo? weather { get; set; }
    public string? Base {get;set;}
    public OWPMain? main { get; set; }
    public double visibility { get; set; }
    public OWPWind? wind { get; set; }
    public OWPClouds? clouds { get; set; }  
    public OWPSys? sys { get; set; }
}
public class OWPHistorialOneCallRes : ApiResponse
{
    public double lat { get; set; }
    public double lon { get; set; }
    public string? timezone { get; set; }
    public double timezone_offset { get; set; }
    public OWPCurrentForecastWeather? current { get; set; }
    public List<OWPHourlyForecastWeather> hourly { get; set; } = new List<OWPHourlyForecastWeather>();
}
#endregion
#region Properties
public class OWPCurrentForecastWeather
{
    public double dt { get;set;}
    public double sunrise { get; set; } 
    public  double sunset { get; set; }
    public double temp { get; set; }
    public double feels_like { get; set; }
    public double pressure { get; set; }
    public double humidity { get; set; }
    public double dew_point { get; set; }
    public double uvi { get; set; }
    public double clouds { get; set; }
    public double visibility { get; set; }
    public double wind_speed { get; set; }  
    public  double wind_deg { get; set; }
    public List<OWPWeatherInfo> weather { get; set; } = new List<OWPWeatherInfo>();

}
public class OWPWeatherInfo
{
    public double id { get; set; }
    public string? main { get; set; }
    public string? description { get; set; }
    public string? icon { get; set; }
}
public class OWPHourlyForecastWeather
{
    public double dt { get; set; }
    public double temp { get; set; }
    public double feels_like { get; set; }
    public double pressure { get; set; }
    public double humidity { get; set; }
    public double dew_point { get; set; }
    public double uvi { get; set; }
    public double clouds { get; set; }
    public double visibility { get; set; }
    public double wind_speed { get; set; }
    public double wind_deg { get; set; }
    public double wind_gust { get; set; }
    public double pop { get; set; }
    public List<OWPWeatherInfo> weather { get; set; }= new List<OWPWeatherInfo>();
}
public class OWPDailyForecastWeather
{
    public double dt { get; set; }
    public double sunrise { get; set; }
    public double sunset { get; set; }
    public double moonrise { get; set; }
    public double moonset { get; set; }
    public double moon_phase { get; set; }
    public OWPTemperature temp { get; set; } = new OWPTemperature();
    public OWPFeelsLike feels_like { get; set; } = new OWPFeelsLike();
    public double pressure { get; set; }
    public double humidity { get; set; }
    public double dew_point { get; set; }
    public double uvi { get; set; }
    public double clouds { get; set; }
    public double wind_speed { get; set; }
    public double wind_deg { get; set; }
    public double wind_gust { get; set; }
    public double pop { get; set; }
    public List<OWPWeatherInfo> weather { get; set; } = new List<OWPWeatherInfo>();
}
public class OWPTemperature
{
    public double day { get; set; }
    public double night { get; set; }
    public double eve { get; set; }
    public double morn { get; set; }
    public double min { get; set; }
    public double max { get; set; }
}
public class OWPFeelsLike
{
    public double day { get; set; }
    public double night { get; set; }
    public double eve { get; set; }
    public double morn { get; set; }
}
public class OWPCoord
{
    public double lon { get; set; }
    public double lat { get; set; }
}
public class OWPMain
{
    public double temp { get; set; }
    public double feels_like { get; set; }
    public double temp_min { get; set; }
    public double temp_max { get; set; }
    public double pressure { get; set; }
    public double humidity { get; set; }
}
public class OWPWind
{
    public double speed { get; set; }
    public double deg { get; set; }
}
public class OWPClouds
{
    public double all { get; set; }
}
public class OWPSys
{
    public double type { get; set; }
    public double id { get; set; }
    public string? country { get; set; }
    public double sunrise { get; set; }
    public double sunset { get; set; }
}
#endregion
