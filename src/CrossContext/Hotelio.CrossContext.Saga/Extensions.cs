using System.Reflection;
using Hotelio.CrossContext.Saga.DAL;
using Hotelio.CrossContext.Saga.ReservationProcess;
using Hotelio.CrossContext.Saga.ReservationProcess.Activity;
using Hotelio.Shared.Configuration;
using MassTransit;
using MassTransit.DependencyInjection;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Hotelio.CrossContext.Saga;

public static class Extensions
{
    public static IServiceCollection AddCrossContextSaga(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<BookResourceOnReservationCreatedActivity>();
        services.AddMassTransit(x =>
        {
            /*x.AddSagaStateMachine<ReservationStateMachine, ReservationState>()
                .InMemoryRepository();*/
            
            x.AddSagaStateMachine<ReservationStateMachine, ReservationState>()
                .EntityFrameworkRepository(r =>
                {
                    r.ConcurrencyMode = ConcurrencyMode.Pessimistic;

                    r.AddDbContext<DbContext, ReservationSagaDbContext>((provider,builder) =>
                    {
                        builder.UseSqlServer(ConfigHelper.GetSqlServerConfig(configuration).ConnectionString, m =>
                        {
                            m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                            m.MigrationsHistoryTable($"__{nameof(ReservationSagaDbContext)}");
                        });
                    });

                    r.UseSqlServer();
                });
            
            x.AddActivitiesFromNamespaceContaining<BookResourceOnReservationCreatedActivity>();
            x.AddActivitiesFromNamespaceContaining<BookResourceOnReservationPayedActivity>();
            x.AddActivitiesFromNamespaceContaining<ConfirmReservationActivity>();
            x.AddActivitiesFromNamespaceContaining<RejectReservationActivity>();
            x.AddActivitiesFromNamespaceContaining<UnBookResourceOnReservationCanceledActivity>();
            
            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
                cfg.ReceiveEndpoint();
                
            });
            
        });

        services.AddDbContext<ReservationSagaDbContext>();
        
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