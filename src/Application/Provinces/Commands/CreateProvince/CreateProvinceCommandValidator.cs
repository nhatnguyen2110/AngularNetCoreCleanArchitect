using FluentValidation;

namespace CleanArchitecture.Application.Provinces.Commands.CreateProvince;

public class CreateProvinceCommandValidator: AbstractValidator<CreateProvinceCommand>
{
    public CreateProvinceCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty();
    }
}
