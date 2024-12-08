using Hotelio.Modules.Pricing.Domain.Repository;
using Hotelio.Modules.Pricing.Infrastructure.DAL;
using Hotelio.Modules.Pricing.Infrastructure.Repository;
using Hotelio.Shared.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Modules.Pricing.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddPricingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PricingDbContext>(options =>
            options.UseSqlServer(ConfigHelper.GetSqlServerConfig(configuration).ConnectionString));
        
        services.AddScoped<IHotelTariffRepository, HotelTariffRepository>();
        
        return services;
    }
    
    public static IApplicationBuilder UsePricingInfrastructure(this IApplicationBuilder app)
    {
        return app;
    }
}