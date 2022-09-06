using CleanArchitecture.Application.Accounts.Dtos;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using SocialNetworkAPI.ApiClient;

namespace CleanArchitecture.Application.Accounts.Commands.FacebookLogin;
public class FacebookLoginCommand : IRequest<Response<SignInResultDto>>
{
    public string Access_Token { get; set; } = default!;
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class FacebookLoginCommandHandler : BaseHandler<FacebookLoginCommand, Response<SignInResultDto>>
{
    private readonly IIdentityService _identityService;
    private readonly ISocialNetworkClient _socialNetworkClient;
    public FacebookLoginCommandHandler(ICommonService commonService,
        ILogger<FacebookLoginCommand> logger,
        IIdentityService identityService,
        ISocialNetworkClient socialNetworkClient
        ) : base(commonService, logger)
    {
        _identityService = identityService;
        _socialNetworkClient = socialNetworkClient;
    }
    public override async Task<Response<SignInResultDto>> Handle(FacebookLoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var fbRes = await this._socialNetworkClient.GetFacebookProfileAsync(
                new SocialNetworkAPI.Models.FBMeReq() { Access_Token = request.Access_Token }
                , cancellationToken);
            if (fbRes == null)
                throw new Exception("Facebook account is null");
            if (string.IsNullOrEmpty(fbRes.email))
                throw new Exception("Cannot retrieve email from Facebook");
            //check if account is existed
            var _account = this._commonService.ApplicationDBContext.Accounts.FirstOrDefault(x => x.Email == fbRes.email);
            if (_account != null)
            {
                //update social token
                var _socialToken = _account.SocialAccountTokens.FirstOrDefault(x => x.LoginProvider == Domain.Enums.LoginProvider.Facebook);
                if (_socialToken != null)
                {
                    _socialToken.Value = request.Access_Token;
                }
                else
                {
                    this._commonService.ApplicationDBContext.SocialAccountTokens.Add(new Domain.Entities.SocialAccountToken()
                    {
                        AccountId = _account.Id,
                        LoginProvider = Domain.Enums.LoginProvider.Facebook,
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
                    FirstName = fbRes.first_name,
                    LastName = fbRes.last_name,
                    AvatarUrl = fbRes.picture?.data?.url,
                    ActivatedEnable = true,
                    SocialAccountTokens = new List<Domain.Entities.SocialAccountToken>()
                };
                _account.SocialAccountTokens.Add(new Domain.Entities.SocialAccountToken()
                {
                    LoginProvider = Domain.Enums.LoginProvider.Facebook,
                    Value = request.Access_Token
                });
                this._commonService.ApplicationDBContext.Accounts.Add(_account);
                await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
            }
            //generate jwt
            var result = await this._identityService.AuthorizeAsync("" + _account.Email, String.Empty, true, Domain.Enums.LoginMethod.Social_Login, cancellationToken);
            result.LoginProvider = LoginProvider.Facebook.ToString();
            return Response<SignInResultDto>.Success(result, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Login. Request: {Name} {@Request}", typeof(FacebookLoginCommand).Name, request);
            return new Response<SignInResultDto>(false, ex.Message, ex.Message, "Failed to Login", request.requestId);
        }
    }
}