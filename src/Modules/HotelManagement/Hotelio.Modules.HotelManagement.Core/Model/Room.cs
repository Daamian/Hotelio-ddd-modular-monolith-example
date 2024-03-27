namespace Hotelio.Modules.HotelManagement.Core.Model;

internal class Room
{
    public int Id { get; set; }
    public int Number { get; set; }
    public int MaxGuests { get; set; }
    public RoomType Type { get; set; }
    public int HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;
}