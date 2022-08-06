namespace CleanArchitecture.Domain.Entities;
public class SysRole
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IList<AccountRole> AccountRoles = new List<AccountRole>();
}
