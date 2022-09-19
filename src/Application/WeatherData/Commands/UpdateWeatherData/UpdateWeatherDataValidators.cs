using System.Text.RegularExpressions;
using FluentValidation;

namespace CleanArchitecture.Application.WeatherData.Commands.UpdateWeatherData;

public class UpdateWeatherDataValidators : AbstractValidator<UpdateWeatherDataCommand>
{
    public UpdateWeatherDataValidators()
    {
        RuleFor(x => x.Weather_main)
            .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.Weather_description)
           .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.Weather_icon)
           .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.WeatherMain_morn)
           .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.WeatherDesc_morn)
           .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.WeatherIcon_morn)
            .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.WeatherMain_day)
           .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.WeatherDesc_day)
           .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.WeatherIcon_day)
            .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.WeatherMain_eve)
           .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.WeatherDesc_eve)
           .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.WeatherIcon_eve)
            .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.WeatherMain_night)
           .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.WeatherDesc_night)
           .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(x => x.WeatherIcon_night)
            .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
    }
}
