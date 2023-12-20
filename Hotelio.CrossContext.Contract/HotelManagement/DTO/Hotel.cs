
namespace Hotelio.CrossContext.Contract.HotelManagement.DTO;

public class Hotel
{
    public readonly string Id;
    public readonly List<Amenity> Amenities;
    public readonly List<RoomType> RoomTypes;

    public Hotel(string id, List<Amenity> amenities, List<RoomType> roomTypes)
    {
        this.Id = id;
        this.Amenities = amenities;
        this.RoomTypes = roomTypes;
    }
}


