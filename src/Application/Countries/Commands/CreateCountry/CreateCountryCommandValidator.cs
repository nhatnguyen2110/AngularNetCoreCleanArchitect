using FluentValidation;

namespace CleanArchitecture.Application.Countries.Commands.CreateCountry;

public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {
        RuleFor(v => v.Name)
            .Matches("^[^<>,<|>]+$")
            .NotEmpty();
        RuleFor(v => v.UserDefined1)
           .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.UserDefined2)
           .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.UserDefined3)
           .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.IconUrl)
           .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.LanguageCode)
          .Matches("^[^<>,<|>]+$");
    }
}
