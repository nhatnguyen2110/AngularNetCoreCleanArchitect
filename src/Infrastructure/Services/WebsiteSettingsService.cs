using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Infrastructure.Services;

public class WebsiteSettingsService : IWebsiteSettingsService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WebsiteSettingsService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetBaseUrl()
    {
        return _httpContextAccessor.HttpContext?.Request.Scheme.ToString() + "://" + _httpContextAccessor.HttpContext?.Request.Host.ToString();
    }
}

