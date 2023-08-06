namespace Hotelio.Modules.Booking.Domain.Model.DTO;
internal class HotelConfig
{
    public readonly List<int> amenities = new List<int>();
    public readonly List<RoomTypeConfig> roomTypes = new List<RoomTypeConfig>();

    public HotelConfig(List<int> amenities, List<RoomTypeConfig> roomTypes)
    {
        this.amenities = amenities;
        this.roomTypes = roomTypes;
    }
}

