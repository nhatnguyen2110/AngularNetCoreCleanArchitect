namespace CleanArchitecture.WebUI.Extensions;

public static partial class CorsExtensions
{
    private const string _defaultCorsPolicyName = Constants.CorsPolicyName;

    public static IServiceCollection AddCorsCustom(this IServiceCollection services, IConfiguration configuration)
    {
        var AllowedHosts = configuration["Cors:Origin"]
                                 .Split(";", StringSplitOptions.RemoveEmptyEntries)
                                 //.Select(o => o.RemovePostFix("/"))
                                 .ToArray();
        if (AllowedHosts.Length != 1 || AllowedHosts[0] != "*")
        {
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(AllowedHosts)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );
        }
        else if (AllowedHosts[0] == "*")
        {
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                )
            );
        }

        return services;
    }

    public static IApplicationBuilder UseCorsCustom(this IApplicationBuilder app)
    {
        app.UseCors(_defaultCorsPolicyName); // Enable CORS!
        return app;
    }
}
