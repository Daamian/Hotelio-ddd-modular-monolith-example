using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Hotelio.Modules.Availability.Test.Unit")]
[assembly: InternalsVisibleTo("Hotelio.Modules.Availability.Application")]
[assembly: InternalsVisibleTo("Hotelio.Modules.Availability.Infrastructure")]
[assembly: InternalsVisibleTo("Hotelio.Modules.Availability.Api")]
[assembly: InternalsVisibleTo("Hotelio.Modules.Availability.Test.Integration")]
[assembly:InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Hotelio.Modules.Availability.Domain;

public static class Extensions
{
    public static IServiceCollection AddAvailabilityDomain(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}