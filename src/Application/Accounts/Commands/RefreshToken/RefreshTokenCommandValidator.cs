using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.RefreshToken;
public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(v => v.AccessToken)
            .NotEmpty();
    }
}
