using Hotelio.AntiCorruptionLayer.Api;
using Hotelio.Bootstrapper.AuthorizationFilter;
using Hotelio.Modules.Availability.Api;
using Hotelio.Modules.Booking.Api;
using Hotelio.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddBookingModule()
    .AddAvailability()
    .AddAntiCorruptionLayer()
    .AddSharedFramework(builder.Configuration);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new InternalControllerAuthorizationFilter());
});

var app = builder.Build();

app.UseSharedFramework();
app.UseAntiCorruptionLayer();
app.UseBookingModule();
app.UseAvailability();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
