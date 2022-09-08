using CleanArchitecture.Application.Accounts.Dtos;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using SocialNetworkAPI.ApiClient;

namespace CleanArchitecture.Application.Accounts.Commands.GoogleLogin;
public class GoogleLoginCommand : IRequest<Response<SignInResultDto>>
{
    public string Access_Token { get; set; } = default!;
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class GoogleLoginCommandHandler : BaseHandler<GoogleLoginCommand, Response<SignInResultDto>>
{
    private readonly IIdentityService _identityService;
    private readonly ISocialNetworkClient _socialNetworkClient;
    public GoogleLoginCommandHandler(ICommonService commonService,
        ILogger<GoogleLoginCommand> logger,
        IIdentityService identityService,
        ISocialNetworkClient socialNetworkClient
        ) : base(commonService, logger)
    {
        _identityService = identityService;
        _socialNetworkClient = socialNetworkClient;
    }

    public async override Task<Response<SignInResultDto>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var fbRes = await this._socialNetworkClient.GetGoogleTokenInfoAsync(
                new SocialNetworkAPI.Models.GoogleTokenInfoReq() { Access_Token = request.Access_Token }
                , cancellationToken);
            if (fbRes == null)
                throw new Exception("Google account is null");
            if (string.IsNullOrEmpty(fbRes.email))
                throw new Exception("Cannot retrieve email from Google");
            //check if account is existed
            var _account = this._commonService.ApplicationDBContext.Accounts.FirstOrDefault(x => x.Email == fbRes.email);
            if (_account != null)
            {
                //update social token
                var _socialToken = this._commonService.ApplicationDBContext.SocialAccountTokens.FirstOrDefault(x => x.AccountId == _account.Id && x.LoginProvider == Domain.Enums.LoginProvider.Google);
                if (_socialToken != null)
                {
                    _socialToken.Value = request.Access_Token;
                }
                else
                {
                    this._commonService.ApplicationDBContext.SocialAccountTokens.Add(new Domain.Entities.SocialAccountToken()
                    {
                        AccountId = _account.Id,
                        LoginProvider = Domain.Enums.LoginProvider.Google,
                        Value = request.Access_Token
                    });
                }
                await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                //create account
                _account = new Domain.Entities.Account()
                {
                    Email = ("" + fbRes.email).Trim().ToLower(),
                    FirstName = fbRes.given_name,
                    LastName = fbRes.family_name,
                    AvatarUrl = fbRes.picture,
                    ActivatedEnable = true,
                    SocialAccountTokens = new List<Domain.Entities.SocialAccountToken>()
                };
                _account.SocialAccountTokens.Add(new Domain.Entities.SocialAccountToken()
                {
                    LoginProvider = Domain.Enums.LoginProvider.Google,
                    Value = request.Access_Token
                });
                this._commonService.ApplicationDBContext.Accounts.Add(_account);
                await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
            }
            //generate jwt
            var result = await this._identityService.AuthorizeAsync("" + _account.Email, String.Empty, true, Domain.Enums.LoginMethod.Social_Login, cancellationToken);
            result.LoginProvider = Domain.Enums.LoginProvider.Google.ToString();
            return Response<SignInResultDto>.Success(result, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Login. Request: {Name} {@Request}", typeof(GoogleLoginCommand).Name, request);
            return new Response<SignInResultDto>(false, ex.Message, ex.Message, "Failed to Login", request.requestId);
        }
    }
}
