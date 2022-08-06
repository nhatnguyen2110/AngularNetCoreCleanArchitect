using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.ResetPassword;
public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(v => v.Code)
            .NotEmpty();
        RuleFor(v => v.NewPassword)
           .NotEmpty()
           .Matches(@"^(?=.*\d)(?=.*[A-Za-z])(?=.*\W).{9,}$");
    }
}
