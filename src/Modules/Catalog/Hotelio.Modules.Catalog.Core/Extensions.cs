using System.Runtime.CompilerServices;
using Hotelio.Modules.Catalog.Core.Repository;
using Hotelio.Modules.Catalog.Core.Repository.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

[assembly: InternalsVisibleTo("Hotelio.Modules.Catalog.Api")]
[assembly: InternalsVisibleTo("Hotelio.Modules.Catalog.Test.Integration")]
namespace Hotelio.Modules.Catalog.Core;

public static class Extensions
{
    public static IServiceCollection AddCatalogCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HotelStoreDBSettings>(
            configuration.GetSection("CatalogStoreDatabase"));

        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<HotelStoreDBSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        services.AddScoped<IHotelRepository, HotelMongoRepository>();

        return services;
    }
    
    public static IApplicationBuilder UseCatalogCore(this IApplicationBuilder app)
    {
        return app;
    }
}