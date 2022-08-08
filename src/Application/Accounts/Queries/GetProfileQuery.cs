using CleanArchitecture.Application.Accounts.Dtos;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Accounts.Queries;
public class GetProfileQuery : IRequest<Response<AccountDto>>
{
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class ResetPasswordCommandHandler : BaseHandler<GetProfileQuery, Response<AccountDto>>
{
    public ResetPasswordCommandHandler(ICommonService commonService,
        ILogger<GetProfileQuery> logger
        ) : base(commonService, logger)
    {
    }
    public async override Task<Response<AccountDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = int.Parse(this._commonService.CurrentUserService.UserId ?? "0");
            var result = await this._commonService.ApplicationDBContext.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
            if (result == null)
            {
                return new Response<AccountDto>(false, "Cannot find user", $"Cannot find user (user id = {userId})", "Failed to get Profile", request.requestId);
            }
            return Response<AccountDto>.Success(_commonService.Mapper.Map<AccountDto>(result), request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get Profile. Request: {Name} {@Request}", typeof(GetProfileQuery).Name, request);
            return new Response<AccountDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to get Profile", request.requestId);
        }
    }
}
