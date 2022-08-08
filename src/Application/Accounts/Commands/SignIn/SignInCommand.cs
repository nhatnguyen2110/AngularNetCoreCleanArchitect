using CleanArchitecture.Application.Accounts.Dtos;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Accounts.Commands.SignIn;
public class SignInCommand : IRequest<Response<SignInResultDto>>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public bool KeepLogin { get; set; } = false;
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class SignInCommandHandler : BaseHandler<SignInCommand, Response<SignInResultDto>>
{
    private readonly IIdentityService _identityService;
    private readonly ApplicationSettings _applicationSettings;
    public SignInCommandHandler(ICommonService commonService,
        ILogger<SignInCommand> logger,
        IIdentityService identityService,
        ApplicationSettings applicationSettings) : base(commonService, logger)
    {
        _identityService = identityService;
        _applicationSettings = applicationSettings;
    }
    public async override Task<Response<SignInResultDto>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var email = request.Email;
            var password = request.Password;
            if (_applicationSettings.EnableEncryptAuthorize)
            {
                try
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    email = CommonHelper.RSADecrypt(request.Email, _applicationSettings.PrivateKeyEncode);
                    password = CommonHelper.RSADecrypt(request.Password, _applicationSettings.PrivateKeyEncode);
#pragma warning restore CS8604 // Possible null reference argument.
                }
                catch (Exception ex)
                {
                    return new Response<SignInResultDto>(false, "can not decrypt email and password", ex.Message, "Failed to Login", request.requestId);
                }
            }
            var result = await this._identityService.AuthorizeAsync(email, password, request.KeepLogin, Domain.Enums.LoginMethod.Email_Password, cancellationToken);
            return Response<SignInResultDto>.Success(result, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Login. Request: {Name} {@Request}", typeof(SignInCommand).Name, request);
            return new Response<SignInResultDto>(false, ex.Message, ex.Message, "Failed to Login", request.requestId);
        }
    }
}