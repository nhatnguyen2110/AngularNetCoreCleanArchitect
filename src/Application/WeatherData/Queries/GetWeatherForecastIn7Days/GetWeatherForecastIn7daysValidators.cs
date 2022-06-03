using FluentValidation;

namespace CleanArchitecture.Application.WeatherData.Queries.GetWeatherForecastIn7Days;

public class GetWeatherForecastIn7daysValidators : AbstractValidator<GetWeatherForecastIn7daysQuery>
{
    public GetWeatherForecastIn7daysValidators()
    {
        RuleFor(x => x.ProvinceId)
           .GreaterThanOrEqualTo(1).WithMessage("Invalid Province Id");
    }
}
