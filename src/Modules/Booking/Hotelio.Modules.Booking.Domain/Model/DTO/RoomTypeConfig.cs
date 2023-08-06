namespace Hotelio.Modules.Booking.Domain.Model.DTO;
internal class RoomTypeConfig
{
    public readonly int maxGuests;

    public RoomTypeConfig(int maxGuests)
    {
        this.maxGuests = maxGuests;
    }
}

