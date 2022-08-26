using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.WeatherData.Commands.UpdateOrCreateWeatherData;
public class UpdateOrCreateWeatherDataCommand : IRequest<Response<int>>
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
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class UpdateOrCreateWeatherDataCommandHandler : BaseHandler<UpdateOrCreateWeatherDataCommand, Response<int>>
{
    public UpdateOrCreateWeatherDataCommandHandler(ICommonService commonService
        , ILogger<UpdateOrCreateWeatherDataCommand> logger
        ) : base(commonService, logger)
    {
    }
    public override async Task<Response<int>> Handle(UpdateOrCreateWeatherDataCommand request, CancellationToken cancellationToken)
    {
        try
        {
            HistoricalWeatherData entity;
            if (request.Id > 0)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                entity = await this._commonService.ApplicationDBContext.HistoricalWeatherDatas
                .FindAsync(new object[] { request.Id }, cancellationToken);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                if (entity == null)
                {
                    throw new NotFoundException(nameof(HistoricalWeatherData), request.Id);
                }
            }
            else
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                entity = await this._commonService.ApplicationDBContext.HistoricalWeatherDatas.FirstOrDefaultAsync(x => x.Dt == request.Dt && x.ProvinceId == request.ProvinceId);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }
            if (entity != null) //edit
            {
                entity.Sunrise = request.Sunrise;
                entity.Sunrise = request.Sunrise;
                entity.Sunset = request.Sunset;
                entity.Temp_avg = request.Temp_avg;
                entity.Temp_min = request.Temp_min;
                entity.Temp_max = request.Temp_max;
                entity.Humidity = request.Humidity;
                entity.Dew_point = request.Dew_point;
                entity.Pop = request.Pop;
                entity.Wind_speed = request.Wind_speed;
                entity.Clouds = request.Clouds;
                entity.Weather_id = request.Weather_id;
                entity.Weather_main = request.Weather_main;
                entity.Weather_description = request.Weather_description;
                entity.Weather_icon = request.Weather_icon;
                entity.Temp_morn = request.Temp_morn;
                entity.Temp_day = request.Temp_day;
                entity.Temp_eve = request.Temp_eve;
                entity.Temp_night = request.Temp_night;
                entity.WeatherId_morn = request.WeatherId_morn;
                entity.WeatherMain_morn = request.WeatherMain_morn;
                entity.WeatherDesc_morn = request.WeatherDesc_morn;
                entity.WeatherIcon_morn = request.WeatherIcon_morn;
                entity.WeatherId_day = request.WeatherId_day;
                entity.WeatherMain_day = request.WeatherMain_day;
                entity.WeatherDesc_day = request.WeatherDesc_day;
                entity.WeatherIcon_day = request.WeatherIcon_day;
                entity.WeatherId_eve = request.WeatherId_eve;
                entity.WeatherMain_eve = request.WeatherMain_eve;
                entity.WeatherDesc_eve = request.WeatherDesc_eve;
                entity.WeatherIcon_eve = request.WeatherIcon_eve;
                entity.WeatherId_night = request.WeatherId_night;
                entity.WeatherMain_night = request.WeatherMain_night;
                entity.WeatherDesc_night = request.WeatherDesc_night;
                entity.WeatherIcon_night = request.WeatherIcon_night;
                await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
                return Response<int>.Success(entity.Id, request.requestId);
            }
            else //create 
            {
                entity = new HistoricalWeatherData()
                {
                    ProvinceId = request.ProvinceId,
                    Dt = request.Dt,
                    Sunrise = request.Sunrise,
                    Sunset = request.Sunset,
                    Temp_avg = request.Temp_avg,
                    Temp_min = request.Temp_min,
                    Temp_max = request.Temp_max,
                    Humidity = request.Humidity,
                    Dew_point = request.Dew_point,
                    Pop = request.Pop,
                    Wind_speed = request.Wind_speed,
                    Clouds = request.Clouds,
                    Weather_id = request.Weather_id,
                    Weather_main = request.Weather_main,
                    Weather_description = request.Weather_description,
                    Weather_icon = request.Weather_icon,
                    Temp_morn = request.Temp_morn,
                    Temp_day = request.Temp_day,
                    Temp_eve = request.Temp_eve,
                    Temp_night = request.Temp_night,
                    WeatherId_morn = request.WeatherId_morn,
                    WeatherMain_morn = request.WeatherMain_morn,
                    WeatherDesc_morn = request.WeatherDesc_morn,
                    WeatherIcon_morn = request.WeatherIcon_morn,
                    WeatherId_day = request.WeatherId_day,
                    WeatherMain_day = request.WeatherMain_day,
                    WeatherDesc_day = request.WeatherDesc_day,
                    WeatherIcon_day = request.WeatherIcon_day,
                    WeatherId_eve = request.WeatherId_eve,
                    WeatherMain_eve = request.WeatherMain_eve,
                    WeatherDesc_eve = request.WeatherDesc_eve,
                    WeatherIcon_eve = request.WeatherIcon_eve,
                    WeatherId_night = request.WeatherId_night,
                    WeatherMain_night = request.WeatherMain_night,
                    WeatherDesc_night = request.WeatherDesc_night,
                    WeatherIcon_night = request.WeatherIcon_night
                };
                this._commonService.ApplicationDBContext.HistoricalWeatherDatas.Add(entity);
                await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
                return Response<int>.Success(entity.Id, request.requestId);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Update Weather Data. Request: {Name} {@Request}", typeof(UpdateOrCreateWeatherDataCommand).Name, request);
            return new Response<int>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Update Weather Data", request.requestId);
        }

    }
}