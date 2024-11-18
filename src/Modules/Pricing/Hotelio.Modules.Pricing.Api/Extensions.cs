using Hotelio.Modules.Pricing.Application;
using Hotelio.Modules.Pricing.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Modules.Pricing.Api;

public static class Extensions
{
    public static IServiceCollection AddBookingModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPricingApplication(configuration);
        services.AddPricingDomain(configuration);
     
        
        return services;
    }

    public static IApplicationBuilder UseBookingModule(this IApplicationBuilder app)
    {
        
        return app;
    }
}