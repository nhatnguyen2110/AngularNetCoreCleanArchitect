using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using OpenWeatherMapAPI.Shared;

namespace CleanArchitecture.Application.GetWeatherConditions.Queries.GetWeatherConditions;

public class GetWeatherConditionQuery : IRequest<Response<WeatherConditionCollectionDto>>
{
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class GetWeatherConditionQueryHandler : BaseHandler<GetWeatherConditionQuery, Response<WeatherConditionCollectionDto>>
{
    public GetWeatherConditionQueryHandler(ICommonService commonService
        , ILogger<GetWeatherConditionQuery> logger
        ) : base(commonService)
    {
    }
    public override Task<Response<WeatherConditionCollectionDto>> Handle(GetWeatherConditionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return Task.FromResult(Response<WeatherConditionCollectionDto>.Success(new WeatherConditionCollectionDto()
            {
                WeatherConditionGroups = OpenWeatherStorage.GetWeatherConditionGroup(),
                WeatherConditions = OpenWeatherStorage.GetWeatherConditions(),
                WeatherConditionsInNight = OpenWeatherStorage.GetWeatherConditions_InNight()
            }, request.requestId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Load Weather Condition. Request: {Name} {@Request}", typeof(GetWeatherConditionQuery).Name, request);
            return Task.FromResult(new Response<WeatherConditionCollectionDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Weather Condition", request.requestId));
        }
    }
}
