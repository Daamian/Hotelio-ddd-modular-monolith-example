using System.Runtime.CompilerServices;
using Hotelio.CrossContext.Contract.Availability;
using Hotelio.Modules.Availability.Api.CrossContext;
using Hotelio.Modules.Availability.Application;
using Hotelio.Modules.Availability.Domain;
using Hotelio.Modules.Availability.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

[assembly: InternalsVisibleTo("Hotelio.Modules.Availability.Test.Integration")]
namespace Hotelio.Modules.Availability.Api;

using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddAvailability(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAvailability, AvailabilityService>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<EventMapper>();
        });
        
        services.AddAvailabilityApplication(configuration);
        services.AddAvailabilityDomain(configuration);
        services.AddAvailabilityInfrastructure(configuration);

        return services;
    }
    
    public static IApplicationBuilder UseAvailability(this IApplicationBuilder app)
    {
        return app;
    }
}