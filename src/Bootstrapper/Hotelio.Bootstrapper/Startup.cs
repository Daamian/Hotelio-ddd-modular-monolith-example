
using Hotelio.Bootstrapper.AuthorizationFilter;
using Hotelio.CrossContext.Contract;
using Hotelio.Modules.HotelManagement.Api;
using Hotelio.Modules.Availability.Api;
using Hotelio.Modules.Booking.Api;
using Hotelio.Shared;

namespace Hotelio.Bootstrapper;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddBookingModule();
        services.AddAvailability();
        services.AddHotelManagementModule();
        services.AddCrossContext();
        services.AddSharedFramework(_configuration);

        services.AddControllersWithViews(options =>
        {
            options.Filters.Add(new InternalControllerAuthorizationFilter());
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSharedFramework();
        app.UseBookingModule();
        app.UseAvailability();
        app.UseHotelManagementModule();
        app.UseCrossContext();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}


