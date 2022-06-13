using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Countries.Commands.DeleteCountry;

public class DeleteCountryCommand : IRequest<Response<Unit>>
{
    public int Id { get; set; }
}
public class DeleteCountryCommandHandler : BaseHandler<DeleteCountryCommand, Response<Unit>>
{
    public DeleteCountryCommandHandler(ICommonService commonService
        ) : base(commonService)
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
            return Response<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Delete Country");
        }
    }
}
