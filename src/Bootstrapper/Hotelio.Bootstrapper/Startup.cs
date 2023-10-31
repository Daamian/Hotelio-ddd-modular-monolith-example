using System;
using Hotelio.Bootstrapper.AuthorizationFilter;
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
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}


