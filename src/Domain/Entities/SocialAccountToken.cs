namespace CleanArchitecture.Domain.Entities;
public class SocialAccountToken : AuditableEntity
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public LoginProvider LoginProvider { get; set; }
    public string? Value { get; set; }
    public Account Account { get; set; } = null!;
}
