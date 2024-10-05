using Hotelio.CrossContext.Contract.Catalog;
using Hotelio.Modules.Catalog.Api.CrossContext.EventHandler;
using Hotelio.Modules.Catalog.Api.CrossContext.Service;
using Hotelio.Modules.Catalog.Core;
using MassTransit;
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
        services.AddMassTransit(x =>
        {
            x.AddConsumer<HotelEventHandler>(config =>
            {
                config.UseConcurrencyLimit(1);
            });
            x.AddConsumer<RoomEventHandler>(config =>
            {
                config.UseConcurrencyLimit(1);
            });
            x.AddConsumer<AvailabilityEventHandler>(config =>
            {
                config.UseConcurrencyLimit(1);
            });
        });
        
        return services;
    }
    
    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        return app;
    }
}