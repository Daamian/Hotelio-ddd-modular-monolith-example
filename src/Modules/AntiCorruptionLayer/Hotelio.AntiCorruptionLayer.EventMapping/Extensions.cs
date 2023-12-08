using Hotelio.AntiCorruptionLayer.EventMapping.Booking;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.AntiCorruptionLayer.EventMapping;

public static class Extensions
{
    public static IServiceCollection AddAntiCorruptionLayerEventMapping(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<AvailabilityEventMapping>();
        });
        
        return services;
    }

    public static IApplicationBuilder UseAntiCorruptionLayerEventMapping(this IApplicationBuilder app)
    {
        return app;
    }
}