using System.Reflection;
using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Application.Common.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        services.Configure<ApplicationSettings>(configuration.GetSection(nameof(ApplicationSettings)))
             .AddScoped(cnf => cnf.GetService<IOptionsSnapshot<ApplicationSettings>>().Value)
             ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        return services;
    }
}
