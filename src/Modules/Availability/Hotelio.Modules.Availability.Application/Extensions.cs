using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Hotelio.Modules.Availability.Infrastructure")]
[assembly: InternalsVisibleTo("Hotelio.Modules.Availability.Api")]
[assembly: InternalsVisibleTo("Hotelio.Modules.Availability.Test.Integration")]
namespace Hotelio.Modules.Availability.Application;

public static class Extensions
{
    public static IServiceCollection AddAvailabilityApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}