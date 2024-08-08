
using Hotelio.Bootstrapper.AuthorizationFilter;
using Hotelio.CrossContext.Contract;
using Hotelio.Modules.HotelManagement.Api;
using Hotelio.Modules.Availability.Api;
using Hotelio.Modules.Booking.Api;
using Hotelio.Modules.Catalog.Api;
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
        services.AddBookingModule(_configuration);
        services.AddAvailability(_configuration);
        services.AddHotelManagementModule(_configuration);
        services.AddCatalogModule(_configuration);
        services.AddCrossContextContract();
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
        app.UseCatalogModule();
        app.UseCrossContextContract();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}


