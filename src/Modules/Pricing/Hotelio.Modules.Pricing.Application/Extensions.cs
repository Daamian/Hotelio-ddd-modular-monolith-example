using System.Runtime.CompilerServices;
using Hotelio.Modules.Pricing.Application.Query.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Hotelio.Modules.Pricing.Api")]
namespace Hotelio.Modules.Pricing.Application;

public static class Extensions
{
    public static IServiceCollection AddPricingApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblyContaining<CalculateHandler>();
        });
        
        return services;
    }
}