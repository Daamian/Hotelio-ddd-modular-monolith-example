using Hotelio.CrossContext.Contract.Catalog;
using Hotelio.Modules.Catalog.Api.CrossContext.EventHandler;
using Hotelio.Modules.Catalog.Api.CrossContext.Service;
using Hotelio.Modules.Catalog.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Modules.Catalog.Api;

public static class Extensions
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICatalogSearcher, CatalogSearcher>();
        services.AddCatalogCore(configuration);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<HotelEventHandler>();
            cfg.RegisterServicesFromAssemblyContaining<RoomEventHandler>();
            cfg.RegisterServicesFromAssemblyContaining<AvailabilityEventHandler>();
        });
        
        return services;
    }
    
    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        return app;
    }
}