using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using OpenWeatherMapAPI.Shared;

namespace CleanArchitecture.Application.GetWeatherConditions.Queries.GetWeatherConditions;

public class GetWeatherConditionQuery : IRequest<Response<WeatherConditionCollectionDto>>
{
}
public class GetWeatherConditionQueryHandler : BaseHandler<GetWeatherConditionQuery, Response<WeatherConditionCollectionDto>>
{
    public GetWeatherConditionQueryHandler(ICommonService commonService
        ) : base(commonService)
    {
    }
    public override Task<Response<WeatherConditionCollectionDto>> Handle(GetWeatherConditionQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Response<WeatherConditionCollectionDto>.Success(new WeatherConditionCollectionDto()
        {
            WeatherConditionGroups = OpenWeatherStorage.GetWeatherConditionGroup(),
            WeatherConditions = OpenWeatherStorage.GetWeatherConditions()
        }));
    }
}
