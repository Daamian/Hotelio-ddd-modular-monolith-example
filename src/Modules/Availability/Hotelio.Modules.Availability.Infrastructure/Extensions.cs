using System.Runtime.CompilerServices;
using Hotelio.Modules.Availability.Application.Command.Handlers;
using Hotelio.Modules.Availability.Application.Query.Handler;
using Hotelio.Modules.Availability.Application.ReadModel;
using Hotelio.Modules.Availability.Domain.Repository;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Modules.Availability.Infrastructure.ReadModel;
using Hotelio.Modules.Availability.Infrastructure.Repository;
using Hotelio.Shared.Configuration;
using Hotelio.Shared.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

[assembly: InternalsVisibleTo("Hotelio.Modules.Availability.Test.Integration")]
namespace Hotelio.Modules.Availability.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddAvailabilityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlServerOptions = ConfigHelper.GetSqlServerConfig(configuration);

        services.AddDbContext<ResourceDbContext>(options =>
            options.UseSqlServer(sqlServerOptions.ConnectionString));

        services.AddScoped<IResourceRepository, EfResourceRepository>();
        services.AddScoped<IResourceStorage>(x => new SqlServerResourceStorage(sqlServerOptions.ConnectionString));

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<BookHandler>();
            cfg.RegisterServicesFromAssemblyContaining<UnBookHandler>();
            cfg.RegisterServicesFromAssemblyContaining<CreateHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetFirstAvailableResourceInDateRangeHandler>();
        });
        
        return services;
    }
}