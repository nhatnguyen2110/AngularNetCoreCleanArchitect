using System.Text.RegularExpressions;
using FluentValidation;

namespace CleanArchitecture.Application.Provinces.Commands.CreateProvince;

public class UpdateProvinceCommandValidator : AbstractValidator<UpdateProvinceCommand>
{
    public UpdateProvinceCommandValidator()
    {
        RuleFor(v => v.Name)
           .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline))
           .NotEmpty();
        RuleFor(v => v.AliasName)
           .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}
