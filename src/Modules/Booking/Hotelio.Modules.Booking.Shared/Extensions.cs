using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Modules.Booking.Shared;

public static class Extensions
{
    public static IServiceCollection AddBookingShared(this IServiceCollection services)
    {
        return services;
    }

    public static IApplicationBuilder UseBookingShared(this IApplicationBuilder app)
    {
        return app;
    }
}
