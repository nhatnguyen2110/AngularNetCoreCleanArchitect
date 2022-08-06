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
        RuleFor(x => x.Weather_main)
            .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.Weather_description)
           .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.Weather_icon)
           .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.WeatherMain_morn)
           .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.WeatherDesc_morn)
           .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.WeatherIcon_morn)
            .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.WeatherMain_day)
           .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.WeatherDesc_day)
           .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.WeatherIcon_day)
            .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.WeatherMain_eve)
           .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.WeatherDesc_eve)
           .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.WeatherIcon_eve)
            .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.WeatherMain_night)
           .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.WeatherDesc_night)
           .Matches("^[^<>,<|>]+$");
        RuleFor(x => x.WeatherIcon_night)
            .Matches("^[^<>,<|>]+$");

    }
}
