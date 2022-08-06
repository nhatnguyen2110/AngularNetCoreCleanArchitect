using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.SignIn;
public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty();
        RuleFor(v => v.Password)
            .NotEmpty();
    }
}
