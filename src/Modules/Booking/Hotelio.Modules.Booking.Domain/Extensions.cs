using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Hotelio.Modules.Booking.Application")]
[assembly: InternalsVisibleTo("Hotelio.Modules.Booking.Infrastructure")]
namespace Hotelio.Modules.Booking.Domain;

public static class Extensions
{
    public static IServiceCollection AddBookingDomain(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}