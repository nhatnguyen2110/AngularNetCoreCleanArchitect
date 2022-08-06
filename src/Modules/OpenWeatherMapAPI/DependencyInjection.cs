using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenWeatherMapAPI.Shared;

namespace OpenWeatherMapAPI;
public static class DependencyInjection
{
    public static IServiceCollection AddOpenWeatherMapModule(this IServiceCollection services, IConfiguration configuration)
    {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        services.Configure<OpenWeatherMapSettings>(configuration.GetSection(nameof(OpenWeatherMapSettings)))
              .AddScoped(cnf => cnf.GetService<IOptionsSnapshot<OpenWeatherMapSettings>>().Value)
              ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        //services.AddHttpClient<IOpenWeatherMapClient, OpenWeatherMapClient>();
        return services;
    }
}
