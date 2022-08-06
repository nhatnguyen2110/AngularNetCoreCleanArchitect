using FluentValidation;

namespace CleanArchitecture.Application.WeatherData.Commands.UpdateWeatherData;

public class UpdateWeatherDataValidators : AbstractValidator<UpdateWeatherDataCommand>
{
    public UpdateWeatherDataValidators()
    {
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
