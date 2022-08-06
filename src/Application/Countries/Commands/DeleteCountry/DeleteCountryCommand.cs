using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Countries.Commands.DeleteCountry;

public class DeleteCountryCommand : IRequest<Response<Unit>>
{
    public int Id { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class DeleteCountryCommandHandler : BaseHandler<DeleteCountryCommand, Response<Unit>>
{
    public DeleteCountryCommandHandler(ICommonService commonService
        , ILogger<DeleteCountryCommand> logger
        ) : base(commonService, logger)
    {
    }
    public async override Task<Response<Unit>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await this._commonService.ApplicationDBContext.Countries
            .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }
            this._commonService.ApplicationDBContext.Countries.Remove(entity);
            await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
            return Response<Unit>.Success(Unit.Value, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Delete Country. Request: {Name} {@Request}", typeof(DeleteCountryCommand).Name, request);
            return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Delete Country", request.requestId);
        }
    }
}
