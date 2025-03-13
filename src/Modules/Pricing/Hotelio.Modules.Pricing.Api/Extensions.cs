using Hotelio.CrossContext.Contract.Pricing;
using Hotelio.Modules.Pricing.Api.CrossContext;
using Hotelio.Modules.Pricing.Application;
using Hotelio.Modules.Pricing.Domain;
using Hotelio.Modules.Pricing.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Modules.Pricing.Api;

public static class Extensions
{
    public static IServiceCollection AddPricingModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPricingService, PricingService>();
        services.AddPricingApplication(configuration);
        services.AddPricingDomain(configuration);
        services.AddPricingInfrastructure(configuration);
        
        return services;
    }

    public static IApplicationBuilder UsePricingModule(this IApplicationBuilder app)
    {
        app.UsePricingInfrastructure();
        return app;
    }
}