namespace CleanArchitecture.Application.Common.Models;
public class UserTokenModel
{
    public string Id { get; set; } = default!;
    public DateTime TokenExpire { get; set; }

}
