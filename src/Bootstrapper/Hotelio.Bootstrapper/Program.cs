using Hotelio.Bootstrapper.AuthorizationFilter;
using Hotelio.Modules.Booking.Api;
using Hotelio.Modules.Booking.Infrastructure;
using Hotelio.Modules.Booking.Shared;
using Hotelio.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBookingModule()
.AddSharedFramework(builder.Configuration);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new InternalControllerAuthorizationFilter());
});

var app = builder.Build();

app.UseSharedFramework();
app.UseBookingModule();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
