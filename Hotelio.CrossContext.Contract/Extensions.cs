using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.CrossContext.Contract;

public static class Extensions
{
    public static IServiceCollection AddCrossContext(this IServiceCollection services)
    {
        return services;
    }

    public static IApplicationBuilder UseCrossContext(this IApplicationBuilder app)
    {
        return app;
    }
}