namespace CleanArchitecture.Application.Configs.Dtos;
public class ConfigsDto
{
    public string? Version { get; set; }
    public bool EnableEncryptAuthorize { get; set; }
    public string? PublicKeyEncode { get; set; }
    public bool EnableGoogleReCaptcha { get; set; }
    public string? GoogleRecaptchaVersion { get; set; }
    public string? GoogleSiteKey { get; set; }
    public string? Facebook_AppId { get; set; }
    public string? Facebook_AppVer { get; set; }

}
