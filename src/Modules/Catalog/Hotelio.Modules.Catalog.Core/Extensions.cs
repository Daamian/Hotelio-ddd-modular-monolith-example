using System.Runtime.CompilerServices;
using Hotelio.Modules.Catalog.Core.Repository;
using Hotelio.Modules.Catalog.Core.Repository.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Hotelio.Modules.Catalog.Api")]
[assembly: InternalsVisibleTo("Hotelio.Modules.Catalog.Test.Integration")]
namespace Hotelio.Modules.Catalog.Core;

public static class Extensions
{
    public static IServiceCollection AddCatalogCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IHotelRepository, HotelMongoRepository>();
        services.Configure<HotelStoreDBSettings>(
            configuration.GetSection("CatalogStoreDatabase"));
        
        return services;
    }
    
    public static IApplicationBuilder UseCatalogCore(this IApplicationBuilder app)
    {
        return app;
    }
}