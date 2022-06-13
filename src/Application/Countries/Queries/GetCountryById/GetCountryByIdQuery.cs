using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Countries.Queries.GetCountriesWithPagination;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Countries.Queries.GetCountryById;

public class GetCountryByIdQuery : IRequest<Response<CountryDto>>
{
    public int CountryId { get; set; }
}
public class GetCountryByIdQueryHandler : BaseHandler<GetCountryByIdQuery, Response<CountryDto>>
{
    public GetCountryByIdQueryHandler(ICommonService commonService
       ) : base(commonService)
    {
    }
    public async override Task<Response<CountryDto>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await this._commonService.ApplicationDBContext.Countries.Include(c => c.Provinces).AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.CountryId);
            if (entity == null)
            {
                return new Response<CountryDto>(false, "Cannot find Country", String.Empty, "Failed to Load Country");
            }
            var result = _commonService.Mapper?.Map<CountryDto>(entity);
#pragma warning disable CS8604 // Possible null reference argument.
            return Response<CountryDto>.Success(result);
#pragma warning restore CS8604 // Possible null reference argument.
        }
        catch (Exception ex)
        {
            return new Response<CountryDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Country");
        }
    }
}
