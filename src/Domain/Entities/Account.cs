namespace CleanArchitecture.Domain.Entities;
public class Account : AuditableEntity
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Gender { get; set; }
    public DateTime? BirthDay { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public int AccessFailedCount { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public bool ActivatedEnable { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public IList<SocialAccountToken> SocialAccountTokens { get; set; } = new List<SocialAccountToken>();
    public IList<AccountRole> AccountRoles { get; set; } = new List<AccountRole>();

}
