using FluentValidation;

namespace CleanArchitecture.Application.Provinces.Commands.CreateProvince;

public class CreateProvinceCommandValidator : AbstractValidator<CreateProvinceCommand>
{
    public CreateProvinceCommandValidator()
    {
        RuleFor(v => v.Name)
            .Matches("^[^<>,<|>]+$")
            .NotEmpty();
        RuleFor(v => v.AliasName)
           .Matches("^[^<>,<|>]+$");
    }
}
