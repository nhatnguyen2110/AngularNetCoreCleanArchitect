using NSwag;
using NSwag.Generation.Processors.Security;

namespace CleanArchitecture.WebUI.Extensions;

public static partial class SwaggerServiceExtensions
{
    private const string _apiVersion = Constants.ApiVersionName.V1;
    private const string _apiName = Constants.ApiVersionName.Name;

    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
    {
        bool.TryParse(configuration["ApplicationSettings:IsUseSwagger"], out bool isUseSwagger);
        if (!isUseSwagger)
            return services;
        services.AddOpenApiDocument(configure =>
        {
            configure.Title = _apiName;
            configure.Version = _apiVersion;
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IConfiguration configuration)
    {
        var isUseSwagger = false;
        bool.TryParse(configuration["ApplicationSettings:IsUseSwagger"], out isUseSwagger);
        if (!isUseSwagger)
            return app;
        app.UseSwaggerUi3(settings =>
        {
            settings.Path = "/api";
            settings.DocumentPath = "/api/specification.json";
        });

        return app;
    }
}