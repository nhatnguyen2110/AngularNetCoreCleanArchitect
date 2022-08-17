using CleanArchitecture.Application.Accounts.Dtos;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Accounts.Commands.SignIn;
public class SignInByEmailVerificationCodeCommand : IRequest<Response<SignInResultDto>>
{
    public string Email { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class SignInByEmailVerificationCodeCommandHandler : BaseHandler<SignInByEmailVerificationCodeCommand, Response<SignInResultDto>>
{
    private readonly IIdentityService _identityService;
    private readonly ApplicationSettings _applicationSettings;
    public SignInByEmailVerificationCodeCommandHandler(
        ICommonService commonService,
        ILogger<SignInByEmailVerificationCodeCommand> logger,
        IIdentityService identityService,
        ApplicationSettings applicationSettings) : base(commonService, logger)
    {
        _identityService = identityService;
        _applicationSettings = applicationSettings;
    }
    public async override Task<Response<SignInResultDto>> Handle(SignInByEmailVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var email = request.Email;
            var code = request.Code;
            if (_applicationSettings.EnableEncryptAuthorize)
            {
                try
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    email = CommonHelper.RSADecrypt(request.Email, _applicationSettings.PrivateKeyEncode);
                    code = CommonHelper.RSADecrypt(request.Code, _applicationSettings.PrivateKeyEncode);
#pragma warning restore CS8604 // Possible null reference argument.
                }
                catch (Exception ex)
                {
                    return new Response<SignInResultDto>(false, "Can not decrypt email and code", ex.Message, "Failed to Login", request.requestId);
                }
            }
            var result = await this._identityService.AuthorizeAsync(email, code, true, Domain.Enums.LoginMethod.Email_VerificationCode, cancellationToken);
            return Response<SignInResultDto>.Success(result, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Login. Request: {Name} {@Request}", typeof(SignInByEmailVerificationCodeCommand).Name, request);
            return new Response<SignInResultDto>(false, ex.Message, ex.Message, "Failed to Login", request.requestId);
        }
    }
}
