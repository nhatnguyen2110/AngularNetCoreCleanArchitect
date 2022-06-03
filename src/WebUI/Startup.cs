using AspNetCoreRateLimit;
using CleanArchitecture.Application;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.WebUI.Extensions;
using CleanArchitecture.WebUI.Filters;
using CleanArchitecture.WebUI.Middlewares;
using CleanArchitecture.WebUI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OpenWeatherMapAPI;
using OpenWeatherMapAPI.ApiClient;
using Serilog;

namespace CleanArchitecture.WebUI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        this.LoggerFactoryInstance = LoggerFactory.Create(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Information);
            builder.AddConsole();
            builder.AddEventSourceLogger();
            builder.AddSerilog();
        });
    }

    public IConfiguration Configuration { get; }

    public ILoggerFactory LoggerFactoryInstance { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure(Configuration);

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => 
            options.SuppressModelStateInvalidFilter = true);

        // In production, the Angular files will be served from this directory
        services.AddSpaStaticFiles(configuration => 
            configuration.RootPath = "ClientApp/dist");

        services.AddMemoryCache();
        services.AddCorsCustom(Configuration); // CORS
        services.AddSwaggerDocumentation(Configuration);
        services.AddOpenWeatherMapModule(Configuration);

        //load general configuration from appsettings.json
        services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
        // inject counter and rules stores
        services.AddInMemoryRateLimiting();
        // configuration (resolvers, counter key builders)
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        // Register Polly retry policies when integrating
        services.AddHttpClient<IOpenWeatherMapClient, OpenWeatherMapClient>().AddPolicyHandlers("RetryPolicyConfig", LoggerFactoryInstance, Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHealthChecks("/health");
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCorsCustom(); // CORS
        app.UseSwaggerDocumentation(Configuration);
        app.UseAddSecurityHeaders(Configuration);
        app.UseIpRateLimiting();

        if (!env.IsDevelopment())
        {
            app.UseSpaStaticFiles();
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });

        app.UseSpa(spa =>
        {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

            if (env.IsDevelopment())
            {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer(Configuration["SpaBaseUrl"] ?? "http://localhost:4200");
            }
        });
    }
}
