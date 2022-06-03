using FluentValidation;

namespace CleanArchitecture.Application.WeatherData.Commands.CreateWeatherData;

public class CreateWeatherDataValidators : AbstractValidator<CreateWeatherDataCommand>
{
    public CreateWeatherDataValidators()
    {
        RuleFor(x => x.ProvinceId)
           .GreaterThanOrEqualTo(1).WithMessage("Invalid Province Id");
        RuleFor(x => x.Dt)
           .GreaterThanOrEqualTo(1).WithMessage("Invalid Dt");
    }
}
