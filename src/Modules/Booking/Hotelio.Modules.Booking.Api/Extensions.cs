using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using System.Runtime.CompilerServices;
using Hotelio.Modules.Booking.Application;
using Hotelio.Modules.Booking.Domain;
using Hotelio.Modules.Booking.Infrastructure;
using Hotelio.Modules.Booking.Shared;
using Microsoft.Extensions.Configuration;

[assembly: InternalsVisibleTo("Hotelio.Bootstrapper")]
namespace Hotelio.Modules.Booking.Api;

public static class Extensions
{
    public static IServiceCollection AddBookingModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBookingApplication(configuration);
        services.AddBookingDomain(configuration);
        services.AddBookingInfrastructure(configuration);
        services.AddBookingShared();
        
        return services;
    }

    public static IApplicationBuilder UseBookingModule(this IApplicationBuilder app)
    {
        app.UseBookingShared();
        
        return app;
    }
}


