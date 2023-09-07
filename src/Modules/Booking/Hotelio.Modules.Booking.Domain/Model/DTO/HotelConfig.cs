namespace Hotelio.Modules.Booking.Domain.Model.DTO;
internal class HotelConfig
{
    public readonly string Id;
    public readonly List<string> Amenities = new List<string>();
    public readonly List<RoomTypeConfig> RoomTypes = new List<RoomTypeConfig>();

    public HotelConfig(string id, List<string> amenities, List<RoomTypeConfig> roomTypes)
    {
        this.Id = id;
        this.Amenities = amenities;
        this.RoomTypes = roomTypes;
    }
}

