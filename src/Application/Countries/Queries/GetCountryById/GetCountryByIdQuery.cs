using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Countries.Queries.GetCountriesWithPagination;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Countries.Queries.GetCountryById;

public class GetCountryByIdQuery : IRequest<Response<CountryDto>>
{
    public int CountryId { get; set; }
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class GetCountryByIdQueryHandler : BaseHandler<GetCountryByIdQuery, Response<CountryDto>>
{
    public GetCountryByIdQueryHandler(ICommonService commonService
        , ILogger<GetCountryByIdQuery> logger
       ) : base(commonService, logger)
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
            return Response<CountryDto>.Success(result, request.requestId);
#pragma warning restore CS8604 // Possible null reference argument.
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load Countries. Request: {Name} {@Request}", typeof(GetCountryByIdQuery).Name, request);
            return new Response<CountryDto>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Country", request.requestId);
        }
    }
}
