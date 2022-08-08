using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.WeatherData.Dtos;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenWeatherMapAPI.Models;
using OpenWeatherMapAPI.Shared;

namespace CleanArchitecture.Application.WeatherData.Queries.GetHistoricalWeatherData;

public class GetHistoricalWeatherDataQuery : IRequest<Response<HistoricalWeatherDto>>
{
    public int ProvinceId { get; set; }
    public double Dt { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class GetHistoricalWeatherDataQueryHandler : BaseHandler<GetHistoricalWeatherDataQuery, Response<HistoricalWeatherDto>>
{
    private readonly OpenWeatherMapSettings _openWeatherMapSettings;
    public GetHistoricalWeatherDataQueryHandler(ICommonService commonService
        , ILogger<GetHistoricalWeatherDataQuery> logger
        , OpenWeatherMapSettings openWeatherMapSettings
        ) : base(commonService, logger)
    {
        this._openWeatherMapSettings = openWeatherMapSettings;
    }
    public override async Task<Response<HistoricalWeatherDto>> Handle(GetHistoricalWeatherDataQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var province = _commonService.ApplicationDBContext.Provinces.AsNoTracking().FirstOrDefault(x => x.Id == request.ProvinceId);
            if (province == null)
            {
                return new Response<HistoricalWeatherDto>(false, $"Invalid Province Id ({request.ProvinceId})", $"Invalid Province Id ({request.ProvinceId})", "Failed to Load Forecast Weather", request.requestId);
            }
            if (!province.Longitude.HasValue || !province.Latitude.HasValue)
            {
                return new Response<HistoricalWeatherDto>(false, $"{province.Name} is not updated the Longitude, Latitude. Please contact Administrator to update", String.Empty, "Failed to Load Forecast Weather", request.requestId);
            }
            var historicalData = await _commonService.OpenWeatherMapClient.GetHistoricalWeatherAsync(new OWPHistoricalWeatherReq()
            {
                Dt = request.Dt,
                Lat = province.Latitude.GetValueOrDefault(0),
                Lon = province.Longitude.GetValueOrDefault(0),
                Units = this._openWeatherMapSettings.units
            });
            var result = _commonService.Mapper?.Map<HistoricalWeatherDto>(historicalData);
            return Response<HistoricalWeatherDto>.Success(result ?? new HistoricalWeatherDto(), request.requestId);
        }
        catch (ApiHttpException ex)
        {
            _logger.LogError(ex, "Failed to Load Forecast Weather. Request: {Name} {@Request}", typeof(GetHistoricalWeatherDataQuery).Name, request);
            return new Response<HistoricalWeatherDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Forecast Weather", request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Load Forecast Weather. Request: {Name} {@Request}", typeof(GetHistoricalWeatherDataQuery).Name, request);
            return new Response<HistoricalWeatherDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Forecast Weather", request.requestId);
        }
    }
}

