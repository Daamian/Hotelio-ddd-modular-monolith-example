using Hotelio.Modules.Availability.Api.Services;
using Hotelio.Modules.Availability.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace Hotelio.Modules.Availability.Api;

using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddAvailability(this IServiceCollection services)
    {
        services.AddScoped<IAvailabilityService, AvailabilityService>();
        services.AddAvailabilityInfrastructure();
        return services;
    }
    
    public static IApplicationBuilder UseAvailability(this IApplicationBuilder app)
    {
        return app;
    }
}