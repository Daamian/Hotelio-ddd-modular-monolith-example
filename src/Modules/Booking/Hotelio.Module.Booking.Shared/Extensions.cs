using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Module.Booking.Shared;

public static class Extensions
{
    public static IServiceCollection AddBookingModule(this IServiceCollection services)
    {
        return services;
    }
}
