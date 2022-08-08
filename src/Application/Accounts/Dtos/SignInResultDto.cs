namespace CleanArchitecture.Application.Accounts.Dtos;
public class SignInResultDto
{
    public string? AccessToken { get; set; }
    public AccountDto? Account { get; set; }

}

