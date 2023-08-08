namespace Hotelio.Modules.Booking.Domain.Model.DTO;
internal class HotelConfig
{
    public readonly string Id;
    public readonly List<string> amenities = new List<string>();
    public readonly List<RoomTypeConfig> roomTypes = new List<RoomTypeConfig>();

    public HotelConfig(string Id, List<string> amenities, List<RoomTypeConfig> roomTypes)
    {
        this.Id = Id;
        this.amenities = amenities;
        this.roomTypes = roomTypes;
    }
}

