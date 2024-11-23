using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Hotelio.Modules.Pricing.Application")]
[assembly: InternalsVisibleTo("Hotelio.Modules.Pricing.Test.Unit")]
namespace Hotelio.Modules.Pricing.Domain;

public static class Extensions
{
    public static IServiceCollection AddPricingDomain(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}