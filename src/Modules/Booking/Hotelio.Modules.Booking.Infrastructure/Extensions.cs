
using Hotelio.Modules.Booking.Application.Command.Handlers;
using Hotelio.Modules.Booking.Application.ReadModel;
using Hotelio.Modules.Booking.Application.Saga;
using Hotelio.Modules.Booking.Domain.Repository;
using Hotelio.Modules.Booking.Infrastructure.ReadModel;
using Hotelio.Modules.Booking.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelio.Modules.Booking.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddBookingInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IReservationRepository, InMemoryReservationRepository>();
        services.AddScoped<IReadModelStorage, InMemoryReadModelStorage>();
        
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblyContaining<CreateReservationHandler>();
            cfg.RegisterServicesFromAssemblyContaining<ConfirmReservationHandler>();
            cfg.RegisterServicesFromAssemblyContaining<PayReservationHandler>();
            cfg.RegisterServicesFromAssemblyContaining<ReservationProcessManager>();
        });
        
        return services;
    }
}
