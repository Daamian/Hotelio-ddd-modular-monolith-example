namespace Hotelio.Modules.Booking.Domain.Model.DTO;
internal class RoomTypeConfig
{
    public readonly int RoomType;
    public readonly int MaxGuests;

    public RoomTypeConfig(int roomType, int maxGuests)
    {
        this.RoomType = roomType;
        this.MaxGuests = maxGuests;
    }
}

