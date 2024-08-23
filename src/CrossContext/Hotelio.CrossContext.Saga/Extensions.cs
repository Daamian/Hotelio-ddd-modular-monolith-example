using Hotelio.CrossContext.Saga.ReservationProcess;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.CrossContext.Saga;

public static class Extensions
{
    public static IServiceCollection AddCrossContextSaga(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblyContaining<ReservationProcessManager>();
        });
        
        return services;
    }

    public static IApplicationBuilder UseCrossContextSaga(this IApplicationBuilder app)
    {
        return app;
    }
}