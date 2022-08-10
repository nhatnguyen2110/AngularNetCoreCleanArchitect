using Serilog;

namespace CleanArchitecture.WebUI;

public class Program
{
    public async static Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        //Initialize Logger
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();

        var host = CreateHostBuilder(args).Build();

        //using (var scope = host.Services.CreateScope())
        //{
        //    var services = scope.ServiceProvider;

        //    try
        //    {
        //        var context = services.GetRequiredService<ApplicationDbContext>();

        //        if (context.Database.IsSqlServer())
        //        {
        //            context.Database.Migrate();
        //        }

        //        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        //        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        //        await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager);
        //        await ApplicationDbContextSeed.SeedSampleDataAsync(context);
        //    }
        //    catch (Exception ex)
        //    {
        //        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        //        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

        //        throw;
        //    }
        //}

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.UseStartup<Startup>());
}
