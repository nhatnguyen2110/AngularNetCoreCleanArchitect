using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.GetEmailVerificationCode;
public class GetEmailVerificationCodeValidator : AbstractValidator<GetEmailVerificationCodeCommand>
{
    public GetEmailVerificationCodeValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
