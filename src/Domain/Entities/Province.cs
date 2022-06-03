namespace CleanArchitecture.Domain.Entities;

public class Province : AuditableEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public int Priority { get; set; }
    public string? AliasName { get; set; }
}
