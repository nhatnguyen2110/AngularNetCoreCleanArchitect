using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.SignIn;
public class SignInByEmailVerificationCodeValidator : AbstractValidator<SignInByEmailVerificationCodeCommand>
{
    public SignInByEmailVerificationCodeValidator(ApplicationSettings applicationSettings)
    {
        RuleFor(v => v.Email)
            .NotEmpty();
        RuleFor(v => v.Code)
            .NotEmpty();
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        RuleFor(v => v.RecaptchaToken).SetValidator(new GoogleCaptchaValidator<SignInByEmailVerificationCodeCommand, string>(applicationSettings));
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
    }
}
