using FluentValidation;

namespace CleanArchitecture.Application.Provinces.Commands.CreateProvince;

public class UpdateProvinceCommandValidator: AbstractValidator<UpdateProvinceCommand>
{
    public UpdateProvinceCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty();
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}
