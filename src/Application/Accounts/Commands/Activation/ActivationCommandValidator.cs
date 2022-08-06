using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.Activation;
public class ActivationCommandValidator : AbstractValidator<ActivationCommand>
{
    public ActivationCommandValidator()
    {
        RuleFor(v => v.code)
            .NotEmpty();
    }
}
