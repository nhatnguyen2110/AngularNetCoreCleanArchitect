using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.ResetPassword;
public class GetEmailResetPasswordValidator : AbstractValidator<GetEmailResetPasswordCommand>
{
    public GetEmailResetPasswordValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
