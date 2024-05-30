using Hotelio.CrossContext.Contract.HotelManagement;
using Hotelio.Modules.HotelManagement.Api.CrossContext;
using Hotelio.Modules.HotelManagement.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Hotelio.Modules.HotelManagement.Api;

public static class Extensions
{
    public static IServiceCollection AddHotelManagementModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IHotelManagement, HotelManagementService>();
        services.AddHotelManagementCore(configuration);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<RoomEventHandler>();
        });
        
        return services;
    }

    public static IApplicationBuilder UseHotelManagementModule(this IApplicationBuilder app)
    {
        return app;
    }
}