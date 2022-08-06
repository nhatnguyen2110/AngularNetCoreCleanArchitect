using FluentValidation;

namespace CleanArchitecture.Application.Accounts.Commands.SignUp;
public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty()
            .Matches("^[^<>,<|>]+$")
            .EmailAddress();
        RuleFor(v => v.Password)
            .NotEmpty()
            .Matches(@"^(?=.*\d)(?=.*[A-Za-z])(?=.*\W).{9,}$");
        RuleFor(v => v.FirstName)
            .NotEmpty()
            .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.LastName)
            .NotEmpty()
            .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.AvatarUrl)
           .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.Gender)
           .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.Address)
           .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.City)
           .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.State)
           .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.ZipCode)
           .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.Country)
           .Matches("^[^<>,<|>]+$");
        RuleFor(v => v.Phone)
           .Matches("^[^<>,<|>]+$");
    }
}
