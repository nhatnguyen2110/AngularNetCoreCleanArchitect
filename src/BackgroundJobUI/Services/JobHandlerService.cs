using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Provinces.Queries.GetProvinceById;
using CleanArchitecture.Application.WeatherData.Commands.CreateWeatherData;
using CleanArchitecture.Application.WeatherData.Queries.GetHistoricalWeatherData;
using CleanArchitecture.Application.WeatherData.Queries.GetLocalDataHistoricalWeather;
using Hangfire.Console;
using Hangfire.Server;
using MediatR;
using Newtonsoft.Json;

namespace BackgroundJobUI.Services;

public interface IJobHandlerService
{
    Task ExecuteUpdateHistoricalWeather(PerformContext context, DateTime? RunDateTime = null);
}
public class JobHandlerService : IJobHandlerService
{
    private ISender _mediator = null!;
    private readonly IServiceProvider _services;
    private readonly IConfiguration _configuration;
    private readonly ILogger<JobHandlerService> _logger;
    private readonly IDateTime _dateTimeService;
    public JobHandlerService(IServiceProvider services
        , IConfiguration configuration
        , ILogger<JobHandlerService> logger
        , IDateTime dateTimeService
        )
    {
        _services = services;
        _configuration = configuration;
        _logger = logger;
        _dateTimeService = dateTimeService;
    }
    protected ISender Mediator => _mediator ??= _services.GetRequiredService<ISender>();
    public async Task ExecuteUpdateHistoricalWeather(PerformContext context, DateTime? RunDateTime = null)
    {
        var strProvinceIds = _configuration["HangfireSettings:UpdateHistoricalWeatherForProvinceIds"];
        var arrProvinces = strProvinceIds.Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (arrProvinces.Length > 0)
        {
            //get yesterday's dt
            var runDateTime = DateTime.Now;
            if (RunDateTime.HasValue)
            {
                runDateTime = RunDateTime.Value;
            }
            var yesterdayTime = runDateTime.AddDays(-1);
            var yesterdayUTC = new DateTime(yesterdayTime.Year, yesterdayTime.Month, yesterdayTime.Day, 0, 0, 0).ToUniversalTime();
            var yesterdayUTCUnix = _dateTimeService.ConvertDatetimeToUnixTimeStamp(yesterdayUTC);

            foreach (var provinceId in arrProvinces)
            {
                var provinceReq = new GetProvinceByIdQuery() { ProvinceId = int.Parse(provinceId) };
                var provinceRes = await Mediator.Send(provinceReq);
                if (provinceRes.Succeeded)
                {
                    if (provinceRes.Data?.Longitude != null && provinceRes.Data?.Latitude != null)
                    {
                        var getLocalDataYesterdayWeatherDataReq = new GetLocalDataHistoricalWeatherQuery()
                        {
                            Dt = yesterdayUTCUnix,
                            ProvinceId = provinceReq.ProvinceId
                        };
                        var getLocalDataYesterdayWeatherDataRes = await Mediator.Send(getLocalDataYesterdayWeatherDataReq);
                        if (!getLocalDataYesterdayWeatherDataRes.Succeeded)//don't find
                        {
                            var historicalDataReq = new GetHistoricalWeatherDataQuery() { Dt = yesterdayUTCUnix, ProvinceId = provinceReq.ProvinceId };
                            var historicalDataRes = await Mediator.Send(historicalDataReq);
                            if (historicalDataRes.Succeeded && historicalDataRes.Data != null)
                            {
                                var historicalData = historicalDataRes.Data;
                                var createWeatherDataCommand = new CreateWeatherDataCommand()
                                {
                                    Dt = yesterdayUTCUnix,
                                    ProvinceId = provinceReq.ProvinceId,
                                    Sunrise = historicalData.Current.Sunrise,
                                    Sunset = historicalData.Current.Sunset,
                                    Temp_avg = Math.Round(historicalData.Hourly.Sum(x => x.Temp) / historicalData.Hourly.Count, 2),
                                    Temp_min = historicalData.Hourly.Min(x => x.Temp),
                                    Temp_max = historicalData.Hourly.Max(x => x.Temp),
                                    Humidity = Math.Round(historicalData.Hourly.Sum(x => x.Humidity) / historicalData.Hourly.Count, 0),
                                    Dew_point = Math.Round(historicalData.Hourly.Sum(x => x.Dew_point) / historicalData.Hourly.Count, 2),
                                    Pop = historicalData.Hourly.Any(x => x.Weather_id < 600) ? 100 : 0,
                                    Wind_speed = Math.Round(historicalData.Hourly.Sum(x => x.Wind_speed) / historicalData.Hourly.Count, 2),
                                    Clouds = Math.Round(historicalData.Hourly.Sum(x => x.Clouds) / historicalData.Hourly.Count, 2),
                                    Weather_id = historicalData.Hourly[historicalData.Hourly.Count / 2].Weather_id,
                                    Weather_main = historicalData.Hourly[historicalData.Hourly.Count / 2].Weather_main,
                                    Weather_description = historicalData.Hourly[historicalData.Hourly.Count / 2].Weather_description,
                                    Weather_icon = historicalData.Hourly[historicalData.Hourly.Count / 2].Weather_icon,
                                    Temp_morn = historicalData.Hourly.First().Temp,
                                    Temp_day = historicalData.Hourly[7].Temp,
                                    Temp_eve = historicalData.Hourly[15].Temp,
                                    Temp_night = historicalData.Hourly.Last().Temp,
                                    WeatherId_morn = historicalData.Hourly.First().Weather_id,
                                    WeatherMain_morn = historicalData.Hourly.First().Weather_main,
                                    WeatherDesc_morn = historicalData.Hourly.First().Weather_description,
                                    WeatherIcon_morn = historicalData.Hourly.First().Weather_icon,
                                    WeatherId_day = historicalData.Hourly[7].Weather_id,
                                    WeatherMain_day = historicalData.Hourly[7].Weather_main,
                                    WeatherDesc_day = historicalData.Hourly[7].Weather_description,
                                    WeatherIcon_day = historicalData.Hourly[7].Weather_icon,
                                    WeatherId_eve = historicalData.Hourly[15].Weather_id,
                                    WeatherMain_eve = historicalData.Hourly[15].Weather_main,
                                    WeatherDesc_eve = historicalData.Hourly[15].Weather_description,
                                    WeatherIcon_eve = historicalData.Hourly[15].Weather_icon,
                                    WeatherId_night = historicalData.Hourly.Last().Weather_id,
                                    WeatherMain_night = historicalData.Hourly.Last().Weather_main,
                                    WeatherDesc_night = historicalData.Hourly.Last().Weather_description,
                                    WeatherIcon_night = historicalData.Hourly.Last().Weather_icon,

                                };
                                var createWeatherDataCommandRes = await Mediator.Send(createWeatherDataCommand);
                                if (!createWeatherDataCommandRes.Succeeded)
                                {
                                    _logger.LogError($"Failed to create Historical Weather Data (Request : {JsonConvert.SerializeObject(createWeatherDataCommand)}). Response: {JsonConvert.SerializeObject(createWeatherDataCommandRes)}");
                                }
                                context.WriteLine($"Complete to update weather data for {provinceRes.Data.Name} (Id = {provinceRes.Data.Id})");
                            }
                            else
                            {
                                context.WriteLine($"Failed to get Historical Data (Request : {JsonConvert.SerializeObject(historicalDataReq)}). Response: {JsonConvert.SerializeObject(historicalDataRes)}");
                                _logger.LogError($"Failed to get Historical Data (Request : {JsonConvert.SerializeObject(historicalDataReq)}). Response: {JsonConvert.SerializeObject(historicalDataRes)}");
                            }
                        }
                        else
                        {
                            context.WriteLine($"Province {provinceRes.Data.Name} (Id = {provinceRes.Data.Id}) was updated before!!!");
                        }

                    }
                    else
                    {
                        context.WriteLine($"Can't complete to update since Longitude/Latitude is empty. Province object: {JsonConvert.SerializeObject(provinceRes.Data)}");
                        _logger.LogWarning($"Can't complete to update since Longitude/Latitude is empty. Province object: {JsonConvert.SerializeObject(provinceRes.Data)}");
                    }
                }
                else
                {
                    context.WriteLine($"Failed to get Province (Request : {JsonConvert.SerializeObject(provinceReq)}). Response: {JsonConvert.SerializeObject(provinceRes)}");
                    _logger.LogError($"Failed to get Province (Request : {JsonConvert.SerializeObject(provinceReq)}). Response: {JsonConvert.SerializeObject(provinceRes)}");
                }
            }
        }
        else
        {
            context.WriteLine("HangfireSettings:UpdateHistoricalWeatherForProvinceIds is empty");
            _logger.LogWarning("HangfireSettings:UpdateHistoricalWeatherForProvinceIds is empty");
        }
    }
}
