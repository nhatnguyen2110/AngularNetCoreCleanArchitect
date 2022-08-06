namespace CleanArchitecture.Domain.Entities;
public class AccountRole
{
    public int AccountId { get; set; }
    public Account? Account { get; set; }
    public int RoleId { get; set; }
    public SysRole? Role { get; set; }

}
