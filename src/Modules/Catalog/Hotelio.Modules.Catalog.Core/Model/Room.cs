namespace Hotelio.Modules.Catalog.Core.Model;

internal class Room
{
    public string Id { get; set; }
    public int MaxGuests { get; set; }
    public string Type { get; set; }
    public List<Reservation> Reservations { get; set; } = new();
}