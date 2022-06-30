
using CleanArchitecture.Application.WeatherData.Queries.GetLocalDataHistoricalWeather;
using FluentValidation;

namespace CleanArchitecture.Application.WeatherData.Queries.GetLastLocalHistoricalWeather;

public class GetLastLocalHistoricalWeatherValidators : AbstractValidator<GetLastLocalHistoricalWeatherQuery>
{
    public GetLastLocalHistoricalWeatherValidators()
    {
        RuleFor(x => x.ProvinceId)
           .GreaterThanOrEqualTo(1).WithMessage("Invalid Province Id");
        RuleFor(x => x.CurrentDt)
           .GreaterThanOrEqualTo(1).WithMessage("Invalid Current Dt");
        RuleFor(x => x.NoOfYearToGet)
           .GreaterThanOrEqualTo(1).WithMessage("Invalid NoOfYearToGet");
    }
}
