using System;
using System.Runtime.CompilerServices;
using Hotelio.Modules.Booking.Application.Client;
using Hotelio.Modules.Booking.Application.ReadModel;
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
        services.AddScoped<IHotelApiClient, HotelApiClient>();
        services.AddScoped<IReadModelStorage, InMemoryReadModelStorage>();
        
        return services;
    }
}
