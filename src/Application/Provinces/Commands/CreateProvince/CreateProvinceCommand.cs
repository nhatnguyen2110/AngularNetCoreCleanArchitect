using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Provinces.Commands.CreateProvince;

public class CreateProvinceCommand : IRequest<Response<int>>
{
    public string? Name { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public int Priority { get; set; }
    public string? AliasName { get; set; }
    public int? CountryId { get; set; }
}
public class CreateProvinceCommandHandler : BaseHandler<CreateProvinceCommand, Response<int>>
{
    public CreateProvinceCommandHandler(ICommonService commonService
        ) : base(commonService)
    {
    }
    public async override Task<Response<int>> Handle(CreateProvinceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new Province()
            {
                Name = request.Name,
                AliasName = request.AliasName,
                Priority = request.Priority,
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                CountryId = request.CountryId
            };
            this._commonService.ApplicationDBContext.Provinces.Add(entity);
            await this._commonService.ApplicationDBContext.SaveChangesAsync(cancellationToken);
            return Response<int>.Success(entity.Id);
        }
        catch (Exception ex)
        {
            return new Response<int>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Create Province");
        }
    }

}