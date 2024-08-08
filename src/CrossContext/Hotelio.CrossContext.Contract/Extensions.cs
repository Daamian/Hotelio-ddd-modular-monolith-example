using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.CrossContext.Contract;

public static class Extensions
{
    public static IServiceCollection AddCrossContextContract(this IServiceCollection services)
    {
        return services;
    }

    public static IApplicationBuilder UseCrossContextContract(this IApplicationBuilder app)
    {
        return app;
    }
}