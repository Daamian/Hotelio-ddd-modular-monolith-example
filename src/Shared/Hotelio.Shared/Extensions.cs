using System;
using Hotelio.Shared.Api;
using Hotelio.Shared.Commands;
using Hotelio.Shared.Event;
using Hotelio.Shared.Queries;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Shared;

public static class Extensions
{
    private const string ApiTitle = "NPay API";
    private const string ApiVersion = "v1";

    public static IServiceCollection AddSharedFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
                    .ConfigureApplicationPartManager(manager =>
                     {
                         manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                     });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ICommandBus, MediatRCommandBus>();
        services.AddScoped<IQueryBus, MediatRQueryBus>();
        services.AddScoped<IEventBus, MediatREventBus>();

        return services;
    }

    public static IApplicationBuilder UseSharedFramework(this IApplicationBuilder app)
    {
        app.UseRouting();

        return app;
    }
}


