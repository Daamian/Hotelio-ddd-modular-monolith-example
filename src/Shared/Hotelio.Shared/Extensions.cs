using Hotelio.Shared.Api;
using Hotelio.Shared.Commands;
using Hotelio.Shared.Event;
using Hotelio.Shared.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hotelio.Shared.SqlServer;
using Microsoft.OpenApi.Models;

namespace Hotelio.Shared;

public static class Extensions
{
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
        services.AddSwaggerGen(swagger =>
        {
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Hotelio API",
                Version = "v1"
            });
        });
        return services;
    }

    public static IApplicationBuilder UseSharedFramework(this IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseSwagger();
        app.UseReDoc(reDoc =>
        {
            reDoc.RoutePrefix = "docs";
            reDoc.SpecUrl = "/swagger/v1/swagger.json";
            reDoc.DocumentTitle = "Hotelio API";
        });

        return app;
    }
}


