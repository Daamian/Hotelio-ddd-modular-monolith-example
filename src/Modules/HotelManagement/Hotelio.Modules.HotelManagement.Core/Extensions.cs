using System.Runtime.CompilerServices;
using Hotelio.Modules.HotelManagement.Core.DAL;
using Hotelio.Modules.HotelManagement.Core.DAL.Repository;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Hotelio.Modules.HotelManagement.Core.Service;
using Hotelio.Shared.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Hotelio.Modules.HotelManagement.Test.Integration")]
namespace Hotelio.Modules.HotelManagement.Core;

public static class Extensions
{
    public static IServiceCollection AddHotelManagementCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<HotelDbContext>(options =>
            options.UseSqlServer(ConfigHelper.GetSqlServerConfig(configuration).ConnectionString));
        
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IHotelService, HotelService>();
        return services;
    }

    public static IApplicationBuilder UseHotelManagementCore(this IApplicationBuilder app)
    {
        return app;
    }
}