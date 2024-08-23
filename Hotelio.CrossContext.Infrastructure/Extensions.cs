using Hotelio.CrossContext.Contract;
using Hotelio.CrossContext.Contract.Shared.Message;
using Hotelio.CrossContext.Infrastructure.Message;
using Hotelio.CrossContext.Saga;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.CrossContext.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddCrossContext(this IServiceCollection services)
    {
        services.AddCrossContextContract();
        services.AddCrossContextSaga();
        services.AddSingleton<IMessageDispatcher, MessageDispatcher>();
        services.AddSingleton<IMessageChannel, MessageChannel>();
        services.AddHostedService<BackgroundServiceChannel>();
        
        return services;
    }

    public static IApplicationBuilder UseCrossContext(this IApplicationBuilder app)
    {
        app.UseCrossContextContract();
        return app;
    }
}