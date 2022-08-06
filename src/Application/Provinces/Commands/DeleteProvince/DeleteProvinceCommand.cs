using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Provinces.Commands.CreateProvince;

public class DeleteProvinceCommand : IRequest<Response<Unit>>
{
    public int Id { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class DeleteProvinceCommandHandler : BaseHandler<DeleteProvinceCommand, Response<Unit>>
{
    public DeleteProvinceCommandHandler(ICommonService commonService
        , ILogger<DeleteProvinceCommand> logger
        ) : base(commonService, logger)
    {
    }
    public async override Task<Response<Unit>> Handle(DeleteProvinceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await this._commonService.ApplicationDBContext.Provinces
            .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Province), request.Id);
            }
            this._commonService.ApplicationDBContext.Provinces.Remove(entity);
            await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
            return Response<Unit>.Success(Unit.Value, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Delete Province. Request: {Name} {@Request}", typeof(CreateProvinceCommand).Name, request);
            return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Delete Province", request.requestId);
        }
    }

}