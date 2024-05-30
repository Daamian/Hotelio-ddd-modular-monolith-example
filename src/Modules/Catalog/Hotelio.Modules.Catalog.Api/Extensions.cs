using Hotelio.CrossContext.Contract.Availability;
using Hotelio.Modules.Catalog.Api.CrossContext.EventHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Modules.Catalog.Api;

public static class Extensions
{
    public static IServiceCollection AddCatalog(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<RoomEventHandler>();
            cfg.RegisterServicesFromAssemblyContaining<HotelEventHandler>();
            cfg.RegisterServicesFromAssemblyContaining<AvailabilityEventHandler>();
        });
        
        return services;
    }
    
    public static IApplicationBuilder UseAvailability(this IApplicationBuilder app)
    {
        return app;
    }
}