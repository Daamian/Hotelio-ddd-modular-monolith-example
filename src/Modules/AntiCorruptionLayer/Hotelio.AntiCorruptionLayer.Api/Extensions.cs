using Hotelio.AntiCorruptionLayer.EventMapping;

namespace Hotelio.AntiCorruptionLayer.Api;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Hotelio.AntiCorruptionLayer.Api.Booking;
using Hotelio.Modules.Booking.Application.Client.Availability;

public static class Extensions
{
    public static IServiceCollection AddAntiCorruptionLayer(this IServiceCollection services)
    {
        services.AddScoped<IAvailabilityApiClient, AvailabilityApiClient>();
        services.AddAntiCorruptionLayerEventMapping();
        return services;
    }

    public static IApplicationBuilder UseAntiCorruptionLayer(this IApplicationBuilder app)
    {
        return app;
    }
}