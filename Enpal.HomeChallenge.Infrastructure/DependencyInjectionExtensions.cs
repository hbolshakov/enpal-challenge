using Enpal.HomeChallenge.Core;
using Enpal.HomeChallenge.Core.Calendar;
using Enpal.HomeChallenge.Infrastructure.Repositories.Slots;
using Enpal.HomeChallenge.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Enpal.HomeChallenge.Infrastructure;

public static class DependencyInjectionExtensions
{
    
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<RootModule>());

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICalendarService, CalendarService>();

        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISlotsRepository, SlotsRepository>();
        
        return services;
    }
}