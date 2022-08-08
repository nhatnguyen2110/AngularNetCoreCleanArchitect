using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Infrastructure.Services;
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public ClaimsPrincipal DecryptTokenToClaim(string decrypt, bool validateLifetime = true)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_configuration["JWTSettings:Key"]));
        var secretKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_configuration["JWTSettings:Secret"]));
        string tokenString = decrypt;

        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidAudiences = new string[] { _configuration["JWTSettings:Audience"] },
            ValidIssuers = new string[] { _configuration["JWTSettings:Issuer"] },
            IssuerSigningKey = securityKey,
            TokenDecryptionKey = secretKey,
            RequireExpirationTime = true,
            ValidateLifetime = validateLifetime
        };

        SecurityToken validatedToken;
        var handler = new JwtSecurityTokenHandler();
        handler.InboundClaimTypeMap.Clear();
        return handler.ValidateToken(tokenString, tokenValidationParameters, out validatedToken);
    }

    public async Task<string> GenerateJWToken(UserTokenModel user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_configuration["JWTSettings:Key"]));
        var secretKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_configuration["JWTSettings:Secret"]));
        var signingCredentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha512);

        var ep = new EncryptingCredentials(
            secretKey,
            SecurityAlgorithms.Aes128KW,
            SecurityAlgorithms.Aes128CbcHmacSha256);

        var handler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = handler.CreateJwtSecurityToken(
            _configuration["JWTSettings:Issuer"],
            _configuration["JWTSettings:Audience"],
            new ClaimsIdentity(await GetClaimsAsync(user)),
            DateTime.Now,
            DateTime.Now.AddMinutes(user.ExpireInMinutes),
            DateTime.Now,
            signingCredentials,
            ep);

        string tokenString = handler.WriteToken(jwtSecurityToken);
        return tokenString;
    }

    public Task<IList<Claim>> GetClaimsAsync(UserTokenModel user)
    {
        IList<Claim> claims = new List<Claim>();
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim("expireInMinutes", user.ExpireInMinutes.ToString()));

        return Task.FromResult(claims);
    }

    public Task<UserTokenModel> GetUserToken(ClaimsPrincipal claimsPrincipal)
    {
        var user = new UserTokenModel();
        user.Id = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Sub);
        user.Email = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Email);
        var expireInMinutesString = claimsPrincipal.FindFirstValue("expireInMinutes");
        if (!string.IsNullOrWhiteSpace(expireInMinutesString))
        {
            int.TryParse(expireInMinutesString, out var expireInMinutes);
            user.ExpireInMinutes = expireInMinutes;
        }
        return Task.FromResult(user);
    }
}
