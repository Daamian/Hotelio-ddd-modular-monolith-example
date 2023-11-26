using Hotelio.Modules.Availability.Domain.Repository;
using Hotelio.Modules.Availability.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Modules.Availability.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddAvailabilityInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IResourceRepository, InMemoryResourceRepository>();
        return services;
    }
}