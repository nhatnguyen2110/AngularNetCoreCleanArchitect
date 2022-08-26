using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.ResetPassword;
public class GetEmailResetPasswordValidator : AbstractValidator<GetEmailResetPasswordCommand>
{
    public GetEmailResetPasswordValidator(ApplicationSettings applicationSettings)
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        RuleFor(v => v.RecaptchaToken).SetValidator(new GoogleCaptchaValidator<GetEmailResetPasswordCommand, string>(applicationSettings));
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
    }
}
