using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Countries.Commands.UpdateCountry;

public class UpdateCountryCommand : IRequest<Response<Unit>>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Priority { get; set; }
    public string? LanguageCode { get; set; }
    public string? IconUrl { get; set; }
    public string? UserDefined1 { get; set; }
    public string? UserDefined2 { get; set; }
    public string? UserDefined3 { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class UpdateCountryCommandHandler : BaseHandler<UpdateCountryCommand, Response<Unit>>
{
    public UpdateCountryCommandHandler(ICommonService commonService
        , ILogger<UpdateCountryCommand> logger
       ) : base(commonService, logger)
    {
    }
    public async override Task<Response<Unit>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await this._commonService.ApplicationDBContext.Countries
            .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }
            entity.Name = request.Name;
            entity.IconUrl = request.IconUrl;
            entity.LanguageCode = request.LanguageCode;
            entity.Priority = request.Priority;
            entity.UserDefined1 = request.UserDefined1;
            entity.UserDefined2 = request.UserDefined2;
            entity.UserDefined3 = request.UserDefined3;

            await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
            return Response<Unit>.Success(Unit.Value, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Delete Country. Request: {Name} {@Request}", typeof(UpdateCountryCommand).Name, request);
            return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Update Country", request.requestId);
        }
    }
}