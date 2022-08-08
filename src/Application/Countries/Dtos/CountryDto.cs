using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Provinces.Dtos;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Countries.Dtos;

public class CountryDto : IMapFrom<Country>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Priority { get; set; }
    public string? LanguageCode { get; set; }
    public string? IconUrl { get; set; }
    public string? UserDefined1 { get; set; }
    public string? UserDefined2 { get; set; }
    public string? UserDefined3 { get; set; }
    public IList<ProvinceDto> Provinces { get; set; } = new List<ProvinceDto>();
}
