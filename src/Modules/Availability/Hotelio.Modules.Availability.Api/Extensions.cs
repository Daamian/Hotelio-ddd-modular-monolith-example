using Hotelio.Modules.Availability.Api.Services;

namespace Hotelio.Modules.Availability.Api;

using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddAvailabilityApi(this IServiceCollection services)
    {
        services.AddScoped<IAvailabilityService, AvailabilityService>();
        return services;
    }
}