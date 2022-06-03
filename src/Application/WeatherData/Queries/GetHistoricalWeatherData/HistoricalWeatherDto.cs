using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using OpenWeatherMapAPI.Models;

namespace CleanArchitecture.Application.WeatherData.Queries.GetHistoricalWeatherData;

public class HistoricalWeatherDto : IMapFrom<OWPHistorialOneCallRes>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OWPHistorialOneCallRes, HistoricalWeatherDto>();
    }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string? Timezone { get; set; }
    public string? Timezone_offset { get; set; }
    public DailyHistoricalWeatherDto Current { get; set; } = new DailyHistoricalWeatherDto();
    public List<HourlyHistoricalWeatherDto> Hourly { get; set; } = new List<HourlyHistoricalWeatherDto>();
}
public class DailyHistoricalWeatherDto : IMapFrom<OWPCurrentForecastWeather>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OWPCurrentForecastWeather, DailyHistoricalWeatherDto>();
    }
    public double Dt { get; set; }
    public double Sunrise { get; set; }
    public double Sunset { get; set; }
}
public class HourlyHistoricalWeatherDto : IMapFrom<OWPHourlyForecastWeather>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OWPHourlyForecastWeather, HourlyHistoricalWeatherDto>()
            .ForMember(d => d.Weather_id, opt => opt.MapFrom(s => s.weather.First().id))
            .ForMember(d => d.Weather_main, opt => opt.MapFrom(s => s.weather.First().main))
            .ForMember(d => d.Weather_description, opt => opt.MapFrom(s => s.weather.First().description))
            .ForMember(d => d.Weather_icon, opt => opt.MapFrom(s => s.weather.First().icon))
            ;
    }
    public double Dt { get; set; }
    public double Temp { get; set; }
    public double Humidity { get; set; }
    public double Dew_point { get; set; }
    public double Clouds { get; set; }
    public double Wind_speed { get; set; }
    public int Weather_id { get; set; }
    public string? Weather_main { get; set; }
    public string? Weather_description { get; set; }
    public string? Weather_icon { get; set; }
}
