using Hotelio.CrossContext.Saga.ReservationProcess;
using Hotelio.CrossContext.Saga.ReservationProcess.Activity;
using MassTransit;
using MassTransit.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Hotelio.CrossContext.Saga;

public static class Extensions
{
    public static IServiceCollection AddCrossContextSaga(this IServiceCollection services)
    {
        services.AddScoped<BookResourceOnReservationCreatedActivity>();
        services.AddMassTransit(x =>
        {
            x.AddSagaStateMachine<ReservationStateMachine, ReservationState>()
                .InMemoryRepository();
            
            x.AddActivitiesFromNamespaceContaining<BookResourceOnReservationCreatedActivity>();
            x.AddActivitiesFromNamespaceContaining<BookResourceOnReservationPayedActivity>();
            x.AddActivitiesFromNamespaceContaining<ConfirmReservationActivity>();
            x.AddActivitiesFromNamespaceContaining<RejectReservationActivity>();
            x.AddActivitiesFromNamespaceContaining<UnBookResourceOnReservationCanceledActivity>();
            
            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });
        
        services.AddLogging(logging =>
        {
            logging.AddSerilog(); // Dodaj Serilog jako logger
        });
        
        
        
        services.AddScoped<ReservationProcessManager>();
        return services;
    }

    public static IApplicationBuilder UseCrossContextSaga(this IApplicationBuilder app)
    {
        return app;
    }
}