using System.Text.RegularExpressions;
using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.SignUp;
public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty()
            .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"))
            .EmailAddress();
        RuleFor(v => v.Password)
            .NotEmpty()
            .Matches(@"^(?=.*\d)(?=.*[A-Za-z])(?=.*\W).{9,}$");
        RuleFor(v => v.FirstName)
            .NotEmpty()
            .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(v => v.LastName)
            .NotEmpty()
            .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(v => v.AvatarUrl)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(v => v.Gender)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(v => v.Address)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(v => v.City)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(v => v.State)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(v => v.ZipCode)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(v => v.Country)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
        RuleFor(v => v.Phone)
           .Must(x => !Regex.IsMatch(x ?? "", "^<.+>+$"));
    }
}
