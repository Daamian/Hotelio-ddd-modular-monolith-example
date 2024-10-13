using Hotelio.CrossContext.Contract;
using Hotelio.CrossContext.Contract.Shared.Message;
using Hotelio.CrossContext.Infrastructure.Message;
using Hotelio.CrossContext.Saga;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.CrossContext.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddCrossContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMessageDispatcher, MassTransitMessageDispatcher>();
        services.AddCrossContextContract();
        services.AddCrossContextSaga(configuration);
        
        return services;
    }

    public static IApplicationBuilder UseCrossContext(this IApplicationBuilder app)
    {
        app.UseCrossContextContract();
        return app;
    }
}