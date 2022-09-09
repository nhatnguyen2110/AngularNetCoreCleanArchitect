using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Files;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CleanArchitectureDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IDomainEventService, DomainEventService>();

        //services
        //    .AddDefaultIdentity<ApplicationUser>()
        //    .AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<ApplicationDbContext>();

        //services.AddIdentityServer()
        //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

        //services.AddAuthentication()
        //    .AddIdentityServerJwt();

        //services.AddAuthorization(options =>
        //    options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        services.AddAuthentication(Constants.CustomAuthenticationScheme)
            .AddScheme<CustomAuthenticationSchemeOptions, CustomAuthenticationHandler>(Constants.CustomAuthenticationScheme, opt => { });

        //InMemory Cache
        services.AddSingleton<ICacheService, CacheService>();
        //Redis Cache
        //services.AddSingleton<ICacheService, RedisCacheService>();
        services.AddScoped<IWebsiteSettingsService, WebsiteSettingsService>();
        services.AddScoped<ICommonService, CommonService>();
        services.AddSingleton<ITokenService, TokenService>();
        services.AddSingleton<IEmailService, EmailService>();

        return services;
    }
}
