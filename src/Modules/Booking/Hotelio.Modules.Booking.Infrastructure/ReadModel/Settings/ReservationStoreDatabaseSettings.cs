namespace Hotelio.Modules.Booking.Infrastructure.ReadModel.Settings;

internal class ReservationStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ReservationsCollectionName { get; set; } = null!;
}