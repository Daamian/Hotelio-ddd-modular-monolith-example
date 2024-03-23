using System.Runtime.CompilerServices;
using Hotelio.Modules.Availability.Application.Command.Handlers;
using Hotelio.Modules.Availability.Application.Query.Handler;
using Hotelio.Modules.Availability.Application.ReadModel;
using Hotelio.Modules.Availability.Domain.Repository;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Modules.Availability.Infrastructure.ReadModel;
using Hotelio.Modules.Availability.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Hotelio.Modules.Availability.Test.Integration")]
namespace Hotelio.Modules.Availability.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddAvailabilityInfrastructure(this IServiceCollection services)
    {
        //TODO move config into config file
        services.AddDbContext<ResourceDbContext>(options =>
            options.UseSqlServer("Server=localhost,1433;Database=master;User=sa;Password=Your_password123;"));
        
        services.AddScoped<IResourceRepository, EFResourceRepository>();
        services.AddScoped<IResourceStorage, SqlServerResourceStorage>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<BookHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetFirstAvailableResourceInDateRangeHandler>();
        });
        
        return services;
    }
}