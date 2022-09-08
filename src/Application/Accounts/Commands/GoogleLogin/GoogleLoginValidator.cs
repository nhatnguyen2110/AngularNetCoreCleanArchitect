using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.GoogleLogin;
public class GoogleLoginValidator : AbstractValidator<GoogleLoginCommand>
{
    public GoogleLoginValidator()
    {
        RuleFor(x => x.Access_Token).NotEmpty();
    }
}
