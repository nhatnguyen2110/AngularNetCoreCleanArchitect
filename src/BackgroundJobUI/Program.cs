using BackgroundJobUI;
using BackgroundJobUI.Extensions;
using BackgroundJobUI.Services;
using CleanArchitecture.Application;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Persistence;
using Hangfire;
using Hangfire.Console;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using OpenWeatherMapAPI;
using OpenWeatherMapAPI.ApiClient;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    // Read from your appsettings.json.
    .AddJsonFile("appsettings.json")
    // Read from your secrets.
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables()
    .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplication(configuration);
builder.Services.AddInfrastructure(configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddOpenWeatherMapModule(configuration);
builder.Services.AddHttpClient<IOpenWeatherMapClient, OpenWeatherMapClient>();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddTransient<IJobHandlerService, JobHandlerService>();
builder.Services.AddSingleton<HangfireSettings>(configuration.GetSection("HangfireSettings").Get<HangfireSettings>());
// Add Hangfire services.
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true,

    })
    .UseConsole()
    );

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt => opt.LoginPath = "/home/login");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseAuthentication();
app.UseStaticFiles();

app.UseRouting();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    //Authorization = new[]
    //    {
    //            new HangfireCustomBasicAuthenticationFilter{
    //                User = configuration.GetSection("HangfireSettings:UserName").Value,
    //                Pass = configuration.GetSection("HangfireSettings:Password").Value
    //            }
    //        }
    Authorization = new[] { new HangfireAuthorizationFilter() }
});


app.UseAuthorization();

app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});

app.MapRazorPages();
app.MapDefaultControllerRoute();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHangfireDashboard();
});
//run job
var jobHandlerService = app.Services.GetService<IJobHandlerService>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
RecurringJob.AddOrUpdate("ExecuteUpdateHistoricalWeather", () => jobHandlerService.ExecuteUpdateHistoricalWeather(null, null), "0 10 * * *", TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();


