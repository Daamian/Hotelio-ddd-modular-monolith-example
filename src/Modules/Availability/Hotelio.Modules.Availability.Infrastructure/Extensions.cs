using Hotelio.Modules.Availability.Application.Command.Handlers;
using Hotelio.Modules.Availability.Application.ReadModel;
using Hotelio.Modules.Availability.Domain.Repository;
using Hotelio.Modules.Availability.Infrastructure.ReadModel;
using Hotelio.Modules.Availability.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Modules.Availability.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddAvailabilityInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IResourceRepository, InMemoryResourceRepository>();
        services.AddScoped<IResourceStorage, InMemoryResourceStorage>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<BookHandler>();
        });
        
        return services;
    }
}