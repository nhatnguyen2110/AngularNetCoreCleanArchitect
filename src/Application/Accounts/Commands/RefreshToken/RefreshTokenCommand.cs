using CleanArchitecture.Application.Accounts.Dtos;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Accounts.Commands.RefreshToken;
public class RefreshTokenCommand : IRequest<Response<SignInResultDto>>
{
    public string? AccessToken { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class RefreshTokenCommandHandler : BaseHandler<RefreshTokenCommand, Response<SignInResultDto>>
{
    private readonly IIdentityService _identityService;
    public RefreshTokenCommandHandler(ICommonService commonService,
        ILogger<RefreshTokenCommand> logger,
        IIdentityService identityService
        ) : base(commonService, logger)
    {
        _identityService = identityService;
    }
    public override async Task<Response<SignInResultDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await this._identityService.RefreshTokenAsync("" + request.AccessToken);
            return Response<SignInResultDto>.Success(result, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to refresh token. Request: {Name} {@Request}", typeof(RefreshTokenCommand).Name, request);
            return new Response<SignInResultDto>(false, ex.Message, ex.Message, "Failed to refresh token", request.requestId);
        }
    }
}