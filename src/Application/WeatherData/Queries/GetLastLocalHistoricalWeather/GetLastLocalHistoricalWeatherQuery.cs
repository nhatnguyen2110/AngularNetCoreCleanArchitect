using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.WeatherData.Queries.GetWeatherForecastIn7Days;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.WeatherData.Queries.GetLocalDataHistoricalWeather;

public class GetLastLocalHistoricalWeatherQuery : IRequest<Response<PaginatedList<DailyForecastWeatherDto>>>
{
    public int ProvinceId { get; set; }
    public double CurrentDt { get; set; }
    public int NoOfYearToGet { get; set; } = 4;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 99;
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class GetLastLocalHistoricalWeatherQueryHandler : BaseHandler<GetLastLocalHistoricalWeatherQuery, Response<PaginatedList<DailyForecastWeatherDto>>>
{
    public GetLastLocalHistoricalWeatherQueryHandler(ICommonService commonService
        , ILogger<GetLastLocalHistoricalWeatherQuery> logger
       ) : base(commonService, logger)
    {
    }
    public async override Task<Response<PaginatedList<DailyForecastWeatherDto>>> Handle(GetLastLocalHistoricalWeatherQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var listDt = new List<double>();
            for (int i = 1; i <= request.NoOfYearToGet; i++)
            {
                listDt.Add(request.CurrentDt - (i * 31536000));
            }
            var result = await _commonService.ApplicationDBContext.HistoricalWeatherDatas.AsNoTracking()
                .Where(x => x.ProvinceId == request.ProvinceId && listDt.Any(t => t == x.Dt))
                .OrderBy(x => x.Dt)
           .ProjectTo<DailyForecastWeatherDto>(this._commonService.Mapper?.ConfigurationProvider)
           .PaginatedListAsync(request.PageNumber, request.PageSize);
            return Response<PaginatedList<DailyForecastWeatherDto>>.Success(result, request.requestId);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Delete Weather Data. Request: {Name} {@Request}", typeof(GetLastLocalHistoricalWeatherQuery).Name, request);
            return new Response<PaginatedList<DailyForecastWeatherDto>>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Data", request.requestId);
        }
    }
}
