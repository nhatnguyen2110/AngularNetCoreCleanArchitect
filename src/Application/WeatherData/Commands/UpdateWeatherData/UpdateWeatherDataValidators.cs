using FluentValidation;

namespace CleanArchitecture.Application.WeatherData.Commands.UpdateWeatherData;

public class UpdateWeatherDataValidators : AbstractValidator<UpdateWeatherDataCommand>
{
    public UpdateWeatherDataValidators()
    {
    }
}
