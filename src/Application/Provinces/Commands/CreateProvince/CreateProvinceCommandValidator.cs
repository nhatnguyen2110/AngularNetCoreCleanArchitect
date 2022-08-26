using System.Text.RegularExpressions;
using FluentValidation;

namespace CleanArchitecture.Application.Provinces.Commands.CreateProvince;

public class CreateProvinceCommandValidator : AbstractValidator<CreateProvinceCommand>
{
    public CreateProvinceCommandValidator()
    {
        RuleFor(v => v.Name)
            .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"))
            .NotEmpty();
        RuleFor(v => v.AliasName)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
    }
}
