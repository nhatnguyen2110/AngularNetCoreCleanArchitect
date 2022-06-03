using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.WeatherData.Queries.GetWeatherForecastIn7Days;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.WeatherData.Queries.GetLocalDataHistoricalWeather;

public class GetLocalDataHistoricalWeatherQuery : IRequest<Response<DailyForecastWeatherDto>>
{
    public int ProvinceId { get; set; }
    public double Dt { get; set; }
}
public class GetLocalDataHistoricalWeatherQueryHandler : BaseHandler<GetLocalDataHistoricalWeatherQuery, Response<DailyForecastWeatherDto>>
{
    public GetLocalDataHistoricalWeatherQueryHandler(ICommonService commonService
       ) : base(commonService)
    {
    }
    public async override Task<Response<DailyForecastWeatherDto>> Handle(GetLocalDataHistoricalWeatherQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var historicalData = await _commonService.ApplicationDBContext.HistoricalWeatherDatas.AsNoTracking().FirstOrDefaultAsync(x => x.Dt == request.Dt && x.ProvinceId == request.ProvinceId);
            if (historicalData == null)
            {
                return new Response<DailyForecastWeatherDto>(false, "Cannot find data", "Cannot find data", "Failed to Load Dataa");
            }
            var result = _commonService.Mapper?.Map<DailyForecastWeatherDto>(historicalData);
            return Response<DailyForecastWeatherDto>.Success(result ?? new DailyForecastWeatherDto());
        }
        catch (Exception ex)
        {
            return new Response<DailyForecastWeatherDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Data");
        }
    }
}
