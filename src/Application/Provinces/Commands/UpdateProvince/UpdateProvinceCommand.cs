using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Provinces.Commands.CreateProvince;

public class UpdateProvinceCommand : IRequest<Response<Unit>>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public int Priority { get; set; }
    public string? AliasName { get; set; }
    public int? CountryId { get; set; }
}
public class UpdateProvinceCommandHandler : BaseHandler<UpdateProvinceCommand, Response<Unit>>
{
    public UpdateProvinceCommandHandler(ICommonService commonService
        ) : base(commonService)
    {
    }
    public async override Task<Response<Unit>> Handle(UpdateProvinceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await this._commonService.ApplicationDBContext.Provinces
            .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Province), request.Id);
            }
            entity.Name = request.Name;
            entity.AliasName = request.AliasName;
            entity.Longitude = request.Longitude;
            entity.Latitude = request.Latitude;
            entity.Priority = request.Priority;
            entity.CountryId = request.CountryId;

            await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
            return Response<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return new Response<Unit>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Update Province");
        }
    }

}