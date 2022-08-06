using FluentValidation;

namespace CleanArchitecture.Application.Provinces.Commands.CreateProvince;

public class UpdateProvinceCommandValidator : AbstractValidator<UpdateProvinceCommand>
{
    public UpdateProvinceCommandValidator()
    {
        RuleFor(v => v.Name)
           .Matches("^[^<>,<|>]+$")
           .NotEmpty();
        RuleFor(v => v.AliasName)
           .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}
