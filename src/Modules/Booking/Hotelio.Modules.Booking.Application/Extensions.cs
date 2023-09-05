using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Hotelio.Modules.Booking.Infrastructure")]
[assembly: InternalsVisibleTo("Hotelio.Modules.Booking.Api")]
namespace Hotelio.Modules.Booking.Application;

using System;
public static class Extensions
{
    public static IServiceCollection AddBookingApplication(IServiceCollection services)
    {
        return services;
    }
}


