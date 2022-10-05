namespace CleanArchitecture.Application.Common.Models;
public class UserTokenModel
{
    public string Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public int ExpireInMinutes { get; set; }
    public List<string> Roles { get; set; } = new List<string>();

}
