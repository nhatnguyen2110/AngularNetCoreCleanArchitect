using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Provinces.Commands.CreateProvince;

public class DeleteProvinceCommand : IRequest<Response<Unit>>
{
    public int Id { get; set; }
}
public class DeleteProvinceCommandHandler : BaseHandler<DeleteProvinceCommand, Response<Unit>>
{
    public DeleteProvinceCommandHandler(ICommonService commonService
        ) : base(commonService)
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
            return Response<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Delete Province");
        }
    }

}