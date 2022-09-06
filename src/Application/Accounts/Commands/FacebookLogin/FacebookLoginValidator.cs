using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.FacebookLogin;
public class FacebookLoginValidator : AbstractValidator<FacebookLoginCommand>
{
    public FacebookLoginValidator()
    {
        RuleFor(x => x.Access_Token).NotEmpty();
    }
}
