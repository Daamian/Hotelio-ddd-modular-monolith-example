namespace Hotelio.Modules.Booking.Domain.Model.DTO;
internal class RoomTypeConfig
{
    public readonly int RoomType;
    public readonly int MaxGuests;
    public readonly int Level;

    public RoomTypeConfig(int roomType, int maxGuests, int level)
    {
        RoomType = roomType;
        MaxGuests = maxGuests;
        Level = level;
    }
}

