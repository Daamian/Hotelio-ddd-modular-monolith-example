using Hotelio.Bootstrapper.AuthorizationFilter;
using Hotelio.CrossContext.Contract;
using Hotelio.CrossContext.Infrastructure;
using Hotelio.Modules.HotelManagement.Api;
using Hotelio.Modules.Availability.Api;
using Hotelio.Modules.Booking.Api;
using Hotelio.Modules.Catalog.Api;
using Hotelio.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddBookingModule(builder.Configuration)
    .AddAvailability(builder.Configuration)
    .AddHotelManagementModule(builder.Configuration)
    .AddCatalogModule(builder.Configuration)
    .AddCrossContext()
    .AddSharedFramework(builder.Configuration);


builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new InternalControllerAuthorizationFilter());
});

var app = builder.Build();

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

app.Run();
