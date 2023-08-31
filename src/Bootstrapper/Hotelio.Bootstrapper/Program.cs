using Hotelio.Modules.Booking.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBookingModule();

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();
