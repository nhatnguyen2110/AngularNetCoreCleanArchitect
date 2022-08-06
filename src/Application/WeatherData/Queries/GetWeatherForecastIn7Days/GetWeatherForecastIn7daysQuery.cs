using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenWeatherMapAPI.Models;
using OpenWeatherMapAPI.Shared;

namespace CleanArchitecture.Application.WeatherData.Queries.GetWeatherForecastIn7Days;

public class GetWeatherForecastIn7daysQuery : IRequest<Response<WeatherForecastDto>>
{
    public int ProvinceId { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class GetWeatherForecastIn7daysQueryHandler : BaseHandler<GetWeatherForecastIn7daysQuery, Response<WeatherForecastDto>>
{
    private readonly OpenWeatherMapSettings _openWeatherMapSettings;
    public GetWeatherForecastIn7daysQueryHandler(ICommonService commonService
        , ILogger<GetWeatherForecastIn7daysQuery> logger
        , OpenWeatherMapSettings openWeatherMapSettings
        ) : base(commonService, logger)
    {
        this._openWeatherMapSettings = openWeatherMapSettings;
    }

    public override async Task<Response<WeatherForecastDto>> Handle(GetWeatherForecastIn7daysQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var province = _commonService.ApplicationDBContext.Provinces.AsNoTracking().FirstOrDefault(x => x.Id == request.ProvinceId);
            if (province == null)
            {
                return new Response<WeatherForecastDto>(false, $"Invalid Province Id ({request.ProvinceId})", $"Invalid Province Id ({request.ProvinceId})", "Failed to Load Forecast Weather", request.requestId);
            }
            if (!province.Longitude.HasValue || !province.Latitude.HasValue)
            {
                return new Response<WeatherForecastDto>(false, $"{province.Name} is not updated the Longitude, Latitude. Please contact Administrator to update", String.Empty, "Failed to Load Forecast Weather", request.requestId);
            }
            var forecastData = await this._commonService.CacheService.GetOrCreateAsync(
                key: $"ForecastWeatherIn7days.{request.ProvinceId}.{DateTime.Now.ToString("ddMMyyyy")}",
                expiryTimeUtc: this._commonService.DateTimeService.GetUTCForEndOfCurrentDate(),
                func: async () =>
                {
                    var r = await _commonService.OpenWeatherMapClient.GetDailyForecastIn7DaysAsync(new OWPDailyForecastIn7DaysReq()
                    {
                        Lat = province.Latitude.GetValueOrDefault(0),
                        Lon = province.Longitude.GetValueOrDefault(0),
                        Units = this._openWeatherMapSettings.units
                    });
                    return r;
                }
            );

            var result = _commonService.Mapper?.Map<WeatherForecastDto>(forecastData);
            //change dt to the begin of day
            if (result != null)
            {
                foreach (var item in result.Daily)
                {
                    var _date = DateTimeOffset.FromUnixTimeSeconds((long)item.Dt).ToLocalTime();
                    var _beginDate = new DateTime(_date.Year, _date.Month, _date.Day, 0, 0, 0);
                    item.Dt = _commonService.DateTimeService.ConvertDatetimeToUnixTimeStamp(_beginDate);
                }
            }

            var currentWeatherForecast = result?.Daily.OrderBy(x => x.Dt).FirstOrDefault();
            if (currentWeatherForecast != null)
            {
                var currentWeatherData = _commonService.ApplicationDBContext.HistoricalWeatherDatas.AsNoTracking().FirstOrDefault(x => x.Dt == currentWeatherForecast.Dt && x.ProvinceId == request.ProvinceId);
                if (currentWeatherData != null)
                {
                    //replace current forecast data by current history data
                    var currentWeather = _commonService.Mapper?.Map<DailyForecastWeatherDto>(currentWeatherData);
                    result?.Daily.Remove(currentWeatherForecast);
#pragma warning disable CS8604 // Possible null reference argument.
                    result?.Daily.Add(currentWeather);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    result.Daily = result.Daily.OrderBy(x => x.Dt).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
            }

            return Response<WeatherForecastDto>.Success(result ?? new WeatherForecastDto(), request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Load Forecast Weather. Request: {Name} {@Request}", typeof(GetWeatherForecastIn7daysQuery).Name, request);
            return new Response<WeatherForecastDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Forecast Weather", request.requestId);
        }
    }
}
