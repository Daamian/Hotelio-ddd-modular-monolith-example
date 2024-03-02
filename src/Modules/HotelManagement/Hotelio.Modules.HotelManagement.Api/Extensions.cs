using Hotelio.CrossContext.Contract.HotelManagement;
using Hotelio.Modules.HotelManagement.Api.CrossContext;
using Hotelio.Modules.HotelManagement.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Modules.HotelManagement.Api;

public static class Extensions
{
    public static IServiceCollection AddHotelManagementModule(this IServiceCollection services)
    {
        services.AddScoped<IHotelManagement, HotelManagementService>();
        services.AddHotelManagementCore();
        return services;
    }

    public static IApplicationBuilder UseHotelManagementModule(this IApplicationBuilder app)
    {
        return app;
    }
}