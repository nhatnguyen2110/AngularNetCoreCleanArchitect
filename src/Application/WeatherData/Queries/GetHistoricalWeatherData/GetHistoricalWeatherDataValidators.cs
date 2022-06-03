using FluentValidation;

namespace CleanArchitecture.Application.WeatherData.Queries.GetHistoricalWeatherData;

public class GetHistoricalWeatherDataValidators : AbstractValidator<GetHistoricalWeatherDataQuery>
{
    public GetHistoricalWeatherDataValidators()
    {
        RuleFor(x => x.ProvinceId)
           .GreaterThanOrEqualTo(1).WithMessage("Invalid Province Id");
        RuleFor(x => x.Dt)
           .GreaterThanOrEqualTo(1).WithMessage("Invalid Dt");
    }
}
