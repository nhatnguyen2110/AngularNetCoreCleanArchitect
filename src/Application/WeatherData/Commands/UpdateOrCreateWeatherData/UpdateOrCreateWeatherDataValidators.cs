using System.Text.RegularExpressions;
using FluentValidation;

namespace CleanArchitecture.Application.WeatherData.Commands.UpdateOrCreateWeatherData;
public class UpdateOrCreateWeatherDataValidators : AbstractValidator<UpdateOrCreateWeatherDataCommand>
{
    public UpdateOrCreateWeatherDataValidators()
    {
        When(x => x.Id == 0, () =>
        {
            RuleFor(x => x.Dt).GreaterThan(0).WithMessage("the field Dt must be greater than 0");
            RuleFor(x => x.ProvinceId).GreaterThan(0).WithMessage("the field ProvinceId must be greater than 0");
        });

        RuleFor(x => x.Weather_main)
            .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.Weather_description)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.Weather_icon)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.WeatherMain_morn)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.WeatherDesc_morn)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.WeatherIcon_morn)
            .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.WeatherMain_day)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.WeatherDesc_day)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.WeatherIcon_day)
            .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.WeatherMain_eve)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.WeatherDesc_eve)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.WeatherIcon_eve)
            .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.WeatherMain_night)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.WeatherDesc_night)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(x => x.WeatherIcon_night)
            .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
    }
}
