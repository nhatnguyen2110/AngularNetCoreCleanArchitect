using System.Security.Claims;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Common.Interfaces;
public interface ITokenService
{
    Task<UserTokenModel> GetUserToken(ClaimsPrincipal claimsPrincipal);
    Task<IList<Claim>> GetClaimsAsync(UserTokenModel user);
    ClaimsPrincipal DecryptTokenToClaim(string decrypt);
    Task<string> GenerateJWToken(UserTokenModel user);
}
