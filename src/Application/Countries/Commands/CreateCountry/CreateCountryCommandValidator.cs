﻿using System.Text.RegularExpressions;
using FluentValidation;

namespace CleanArchitecture.Application.Countries.Commands.CreateCountry;

public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {
        RuleFor(v => v.Name)
            .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"))
            .NotEmpty();
        RuleFor(v => v.UserDefined1)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(v => v.UserDefined2)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(v => v.UserDefined3)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(v => v.IconUrl)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(v => v.LanguageCode)
          .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
    }
}
