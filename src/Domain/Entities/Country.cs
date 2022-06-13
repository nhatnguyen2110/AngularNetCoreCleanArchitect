namespace CleanArchitecture.Domain.Entities;

public class Country : AuditableEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Priority { get; set; }
    public string? LanguageCode { get; set; }
    public string? IconUrl { get; set; }
    public string? UserDefined1 { get; set; }
    public string? UserDefined2 { get; set; }
    public string? UserDefined3 { get; set; }
    public IList<Province> Provinces { get; set; } = new List<Province>();

}
