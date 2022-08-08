using System.Text.Encodings.Web;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace CleanArchitecture.Infrastructure.Identity;
public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthenticationSchemeOptions>
{
    private readonly ITokenService _tokenService;
    public CustomAuthenticationHandler(
            IOptionsMonitor<CustomAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ITokenService tokenService)
            : base(options, logger, encoder, clock)
    {
        _tokenService = tokenService;
    }
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        UserTokenModel? user = null;

        // validation comes in here
        if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            return AuthenticateResult.Fail("Header Not Found.");
        }
        var header = Request.Headers[HeaderNames.Authorization].ToString();
        if (string.IsNullOrWhiteSpace(header) || header.IndexOf("Bearer ") < 0)
        {
            return AuthenticateResult.Fail("Header Not Found.");
        }
        try
        {
            var token = header.Replace("Bearer ", "");
            var claimsPrincipal = _tokenService.DecryptTokenToClaim(token);
            if (claimsPrincipal == null)
            {
                return AuthenticateResult.Fail("Can't DecryptTokenToClaim.");
            }
            user = await _tokenService.GetUserToken(claimsPrincipal);
            // generate AuthenticationTicket from the Identity
            // and current authentication scheme
            var ticket = new AuthenticationTicket(claimsPrincipal, this.Scheme.Name);

            // pass on the ticket to the middleware
            return AuthenticateResult.Success(ticket);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to decrypt token: " + ex.Message);
            return AuthenticateResult.Fail("TokenParseException");
        }
    }
}
