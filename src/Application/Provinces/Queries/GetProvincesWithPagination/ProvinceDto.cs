using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Provinces.Queries.GetProvincesWithPagination;

public class ProvinceDto : IMapFrom<Province>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? AliasName { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public int Priority { get; set; }
}
