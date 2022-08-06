using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.SignIn;
public class SignInByEmailVerificationCodeValidator : AbstractValidator<SignInByEmailVerificationCodeCommand>
{
    public SignInByEmailVerificationCodeValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty();
        RuleFor(v => v.Code)
            .NotEmpty();
    }
}
