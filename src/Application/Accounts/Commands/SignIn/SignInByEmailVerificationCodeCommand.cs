using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Accounts.Commands.SignIn;
public class SignInByEmailVerificationCodeCommand : IRequest<Response<SignInResultDto>>
{
    public string Email { get; set; } = default!;
    public string Code { get; set; } = default!;
    public bool KeepLogin { get; set; } = false;
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class SignInByEmailVerificationCodeCommandHandler : BaseHandler<SignInByEmailVerificationCodeCommand, Response<SignInResultDto>>
{
    private readonly IIdentityService _identityService;
    public SignInByEmailVerificationCodeCommandHandler(
        ICommonService commonService,
        ILogger<SignInByEmailVerificationCodeCommand> logger,
        IIdentityService identityService) : base(commonService, logger)
    {
        _identityService = identityService;
    }
    public async override Task<Response<SignInResultDto>> Handle(SignInByEmailVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await this._identityService.AuthorizeAsync(request.Email, request.Code, request.KeepLogin, Domain.Enums.LoginMethod.Email_VerificationCode, cancellationToken);
            return Response<SignInResultDto>.Success(result, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Login. Request: {Name} {@Request}", typeof(SignInByEmailVerificationCodeCommand).Name, request);
            return new Response<SignInResultDto>(false, ex.Message, ex.Message, "Failed to Login", request.requestId);
        }
    }
}
