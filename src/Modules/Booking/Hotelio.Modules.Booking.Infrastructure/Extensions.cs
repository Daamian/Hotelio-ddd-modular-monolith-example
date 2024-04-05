using Hotelio.Modules.Booking.Application.Command.Handlers;
using Hotelio.Modules.Booking.Application.ReadModel;
using Hotelio.Modules.Booking.Application.ReadModel.Projector;
using Hotelio.Modules.Booking.Application.Saga;
using Hotelio.Modules.Booking.Domain.Repository;
using Hotelio.Modules.Booking.Infrastructure.DAL;
using Hotelio.Modules.Booking.Infrastructure.ReadModel;
using Hotelio.Modules.Booking.Infrastructure.ReadModel.Settings;
using Hotelio.Modules.Booking.Infrastructure.Repository;
using Hotelio.Shared.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Modules.Booking.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddBookingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ReservationDbContext>(options =>
            options.UseSqlServer(ConfigHelper.GetSqlServerConfig(configuration).ConnectionString));
        
        services.AddScoped<IReservationRepository, EFReservationRepository>();
        services.AddScoped<IReadModelStorage, MongoReadModelStorage>();
        
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblyContaining<CreateReservationHandler>();
            cfg.RegisterServicesFromAssemblyContaining<ConfirmReservationHandler>();
            cfg.RegisterServicesFromAssemblyContaining<PayReservationHandler>();
            cfg.RegisterServicesFromAssemblyContaining<ReservationProcessManager>();
            cfg.RegisterServicesFromAssemblyContaining<ReservationReadModelProjector>();
        });
        
        services.Configure<ReservationStoreDatabaseSettings>(
            configuration.GetSection("ReservationStoreDatabase"));
        
        return services;
    }
    
    public static IApplicationBuilder UseBookingInfrastructure(this IApplicationBuilder app)
    {
        return app;
    }
}
