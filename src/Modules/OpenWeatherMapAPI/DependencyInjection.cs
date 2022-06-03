using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenWeatherMapAPI.ApiClient;
using OpenWeatherMapAPI.Shared;

namespace OpenWeatherMapAPI;
public static class DependencyInjection
{
    public static IServiceCollection AddOpenWeatherMapModule(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<OpenWeatherMapSettings>(configuration.GetSection(nameof(OpenWeatherMapSettings)))
              .AddScoped(cnf => cnf.GetService<IOptionsSnapshot<OpenWeatherMapSettings>>().Value)
              ;
        //services.AddHttpClient<IOpenWeatherMapClient, OpenWeatherMapClient>();
        return services;
    }
}
