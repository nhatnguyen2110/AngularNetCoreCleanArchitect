using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SocialNetworkAPI.Shared;

namespace SocialNetworkAPI;
public static class DependencyInjection
{
    public static IServiceCollection AddSocialNetworkModule(this IServiceCollection services, IConfiguration configuration)
    {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        services.Configure<SocialNetworkConfig>(configuration.GetSection(nameof(SocialNetworkConfig)))
              .AddScoped(cnf => cnf.GetService<IOptionsSnapshot<SocialNetworkConfig>>().Value)
              ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        return services;
    }
}
