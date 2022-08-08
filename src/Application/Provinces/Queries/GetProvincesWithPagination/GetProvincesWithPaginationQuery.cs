using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Handlers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Provinces.Dtos;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Provinces.Queries.GetProvincesWithPagination;

public class GetProvincesWithPaginationQuery : IRequest<Response<PaginatedList<ProvinceDto>>>
{
    public string? Keyword { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 99;
    public string requestId { get; set; } = Guid.NewGuid().ToString();
}
public class GetProvincesWithPaginationQueryHandler : BaseHandler<GetProvincesWithPaginationQuery, Response<PaginatedList<ProvinceDto>>>
{

    public GetProvincesWithPaginationQueryHandler(ICommonService commonService
        , ILogger<GetProvincesWithPaginationQuery> logger
        ) : base(commonService, logger)
    {
    }

    public override async Task<Response<PaginatedList<ProvinceDto>>> Handle(GetProvincesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await this._commonService.ApplicationDBContext.Provinces
           .AsNoTracking()
           .Where(x =>
           string.IsNullOrEmpty(request.Keyword)
           || (x.Name + "").ToLower().Contains(request.Keyword.ToLower())
           || (x.AliasName + "").ToLower().Contains(request.Keyword.ToLower())
           )
           .OrderBy(x => x.Priority).ThenBy(x => x.Name)
           .ProjectTo<ProvinceDto>(this._commonService.Mapper?.ConfigurationProvider)
           .PaginatedListAsync(request.PageNumber, request.PageSize);
            return Response<PaginatedList<ProvinceDto>>.Success(result, request.requestId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Load Provinces. Request: {Name} {@Request}", typeof(GetProvincesWithPaginationQuery).Name, request);
            return new Response<PaginatedList<ProvinceDto>>(false, Constants.GeneralErrorMessage, ex.Message, "Failed to Load Provinces", request.requestId);
        }
    }
}