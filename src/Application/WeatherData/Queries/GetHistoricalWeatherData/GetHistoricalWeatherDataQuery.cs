using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenWeatherMapAPI.Models;
using OpenWeatherMapAPI.Shared;

namespace CleanArchitecture.Application.WeatherData.Queries.GetHistoricalWeatherData;

public class GetHistoricalWeatherDataQuery: IRequest<Response<HistoricalWeatherDto>>
{
    public int ProvinceId { get; set; }
    public double Dt { get; set; }
}
public class GetHistoricalWeatherDataQueryHandler : BaseHandler<GetHistoricalWeatherDataQuery, Response<HistoricalWeatherDto>>
{
    private readonly OpenWeatherMapSettings _openWeatherMapSettings;
    public GetHistoricalWeatherDataQueryHandler(ICommonService commonService,
        OpenWeatherMapSettings openWeatherMapSettings
        ) : base(commonService)
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
                return new Response<HistoricalWeatherDto>(false, $"Invalid Province Id ({request.ProvinceId})", $"Invalid Province Id ({request.ProvinceId})", "Failed to Load Forecast Weather");
            }
            if (!province.Longitude.HasValue || !province.Latitude.HasValue)
            {
                return new Response<HistoricalWeatherDto>(false, $"{province.Name} is not updated the Longitude, Latitude. Please contact Administrator to update", String.Empty, "Failed to Load Forecast Weather");
            }
            var historicalData = await _commonService.OpenWeatherMapClient.GetHistoricalWeatherAsync(new OWPHistoricalWeatherReq()
            {
                Dt = request.Dt,
                Lat = province.Latitude.GetValueOrDefault(0),
                Lon = province.Longitude.GetValueOrDefault(0),
                Units = this._openWeatherMapSettings.units
            });
            var result = _commonService.Mapper?.Map<HistoricalWeatherDto>(historicalData);
            return Response<HistoricalWeatherDto>.Success(result ?? new HistoricalWeatherDto());
        }
        catch (ApiHttpException ex)
        {
            return new Response<HistoricalWeatherDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Forecast Weather");
        }
        catch (Exception ex)
        {
            return new Response<HistoricalWeatherDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Forecast Weather");
        }
    }
}

