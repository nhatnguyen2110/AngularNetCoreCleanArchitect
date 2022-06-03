using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using OpenWeatherMapAPI.Models;

namespace CleanArchitecture.Application.WeatherData.Queries.GetWeatherForecastIn7Days;

public class WeatherForecastDto : IMapFrom<OWPOneCallRes>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OWPOneCallRes, WeatherForecastDto>();
    }
    public List<DailyForecastWeatherDto> Daily { get; set; } = new List<DailyForecastWeatherDto>();

}

public class DailyForecastWeatherDto : IMapFrom<OWPDailyForecastWeather>, IMapFrom<HistoricalWeatherData>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OWPDailyForecastWeather, DailyForecastWeatherDto>()
            .ForMember(d => d.Temp_avg, opt => opt.MapFrom(s => Math.Round((s.temp.min + s.temp.max) / 2, 2)))
            .ForMember(d => d.Temp_min, opt => opt.MapFrom(s => s.temp.min))
            .ForMember(d => d.Temp_max, opt => opt.MapFrom(s => s.temp.max))
            .ForMember(d => d.Weather_id, opt => opt.MapFrom(s => s.weather.First().id))
            .ForMember(d => d.Weather_main, opt => opt.MapFrom(s => s.weather.First().main))
            .ForMember(d => d.Weather_description, opt => opt.MapFrom(s => s.weather.First().description))
            .ForMember(d => d.Weather_icon, opt => opt.MapFrom(s => s.weather.First().icon))
            .ForMember(d => d.Temp_morn, opt => opt.MapFrom(s => s.temp.min))
            .ForMember(d => d.Temp_day, opt => opt.MapFrom(s => s.temp.max))
            .ForMember(d => d.Temp_eve, opt => opt.MapFrom(s => s.temp.max))
            .ForMember(d => d.Temp_night, opt => opt.MapFrom(s => s.temp.min))
            .ForMember(d => d.WeatherId_morn, opt => opt.MapFrom(s => s.weather.First().id))
            .ForMember(d => d.WeatherMain_morn, opt => opt.MapFrom(s => s.weather.First().main))
            .ForMember(d => d.WeatherDesc_morn, opt => opt.MapFrom(s => s.weather.First().description))
            .ForMember(d => d.WeatherIcon_morn, opt => opt.MapFrom(s => s.weather.First().icon))
            .ForMember(d => d.WeatherId_day, opt => opt.MapFrom(s => s.weather.First().id))
            .ForMember(d => d.WeatherMain_day, opt => opt.MapFrom(s => s.weather.First().main))
            .ForMember(d => d.WeatherDesc_day, opt => opt.MapFrom(s => s.weather.First().description))
            .ForMember(d => d.WeatherIcon_day, opt => opt.MapFrom(s => s.weather.First().icon))
            .ForMember(d => d.WeatherId_eve, opt => opt.MapFrom(s => s.weather.First().id))
            .ForMember(d => d.WeatherMain_eve, opt => opt.MapFrom(s => s.weather.First().main))
            .ForMember(d => d.WeatherDesc_eve, opt => opt.MapFrom(s => s.weather.First().description))
            .ForMember(d => d.WeatherIcon_eve, opt => opt.MapFrom(s => s.weather.First().icon))
            .ForMember(d => d.WeatherId_night, opt => opt.MapFrom(s => s.weather.First().id))
            .ForMember(d => d.WeatherMain_night, opt => opt.MapFrom(s => s.weather.First().main))
            .ForMember(d => d.WeatherDesc_night, opt => opt.MapFrom(s => s.weather.First().description))
            .ForMember(d => d.WeatherIcon_night, opt => opt.MapFrom(s => s.weather.First().icon))
            ;

        profile.CreateMap<HistoricalWeatherData, DailyForecastWeatherDto>();
    }
    public int Id { get; set; }
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
    
}
