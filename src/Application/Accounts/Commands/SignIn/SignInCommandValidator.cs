using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.SignIn;
public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator(ApplicationSettings applicationSettings)
    {

        RuleFor(v => v.Email)
            .NotEmpty();
        RuleFor(v => v.Password)
            .NotEmpty();
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        RuleFor(v => v.RecaptchaToken).SetValidator(new GoogleCaptchaValidator<SignInCommand, string>(applicationSettings));
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
    }
}
