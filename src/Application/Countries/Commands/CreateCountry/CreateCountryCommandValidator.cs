using FluentValidation;

namespace CleanArchitecture.Application.Countries.Commands.CreateCountry;

public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty();
    }
}
