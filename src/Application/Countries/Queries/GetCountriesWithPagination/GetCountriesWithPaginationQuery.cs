using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Countries.Queries.GetCountriesWithPagination;

public class GetCountriesWithPaginationQuery : IRequest<Response<PaginatedList<CountryDto>>>
{
    public string? Keyword { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 99;
}
public class GetCountriesWithPaginationQueryHandler : BaseHandler<GetCountriesWithPaginationQuery, Response<PaginatedList<CountryDto>>>
{
    public GetCountriesWithPaginationQueryHandler(ICommonService commonService
       ) : base(commonService)
    {
    }
    public async override Task<Response<PaginatedList<CountryDto>>> Handle(GetCountriesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await this._commonService.ApplicationDBContext.Countries
           .AsNoTracking()
           .Where(x =>
           string.IsNullOrEmpty(request.Keyword)
           || (x.Name + "").ToLower().Contains(request.Keyword.ToLower())
           || (x.LanguageCode + "").ToLower().Contains(request.Keyword.ToLower())
           )
           .OrderBy(x => x.Priority).ThenBy(x => x.Name)
           .ProjectTo<CountryDto>(this._commonService.Mapper?.ConfigurationProvider)
           .PaginatedListAsync(request.PageNumber, request.PageSize);
            return Response<PaginatedList<CountryDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return new Response<PaginatedList<CountryDto>>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to load Countries");
        }
    }
}
