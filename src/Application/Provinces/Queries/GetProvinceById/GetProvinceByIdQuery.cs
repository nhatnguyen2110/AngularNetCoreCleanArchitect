using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Provinces.Queries.GetProvincesWithPagination;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Provinces.Queries.GetProvinceById;

public class GetProvinceByIdQuery : IRequest<Response<ProvinceDto>>
{
    public int ProvinceId { get; set; }
}
public class GetProvinceByIdQueryHandler : BaseHandler<GetProvinceByIdQuery, Response<ProvinceDto>>
{
    public GetProvinceByIdQueryHandler(ICommonService commonService
        ) : base(commonService)
    {
    }
    public async override Task<Response<ProvinceDto>> Handle(GetProvinceByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await this._commonService.ApplicationDBContext.Provinces.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.ProvinceId);
            if (entity == null)
            {
                return new Response<ProvinceDto>(false, "Cannot find Province", String.Empty, "Failed to Load Province");
            }
            var result = _commonService.Mapper?.Map<ProvinceDto>(entity);
#pragma warning disable CS8604 // Possible null reference argument.
            return Response<ProvinceDto>.Success(result);
#pragma warning restore CS8604 // Possible null reference argument.
        }
        catch (Exception ex)
        {
            return new Response<ProvinceDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Province");
        }
    }
}
