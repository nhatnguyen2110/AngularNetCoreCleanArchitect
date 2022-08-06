using CleanArchitecture.Application.Accounts.Commands.SignIn;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);

    Task<SignInResultDto> AuthorizeAsync(string emailOrPhoneNo, string passcode, bool keepLogin, LoginMethod loginMethod, CancellationToken cancellationToken);
}
