
namespace Hotelio.CrossContext.Contract.HotelManagement.DTO;

public class Hotel
{
    public readonly string Id;
    public readonly string Name;
    public readonly List<Amenity> Amenities;
    public readonly List<RoomType> RoomTypes;

    public Hotel(string id, string name, List<Amenity> amenities, List<RoomType> roomTypes)
    {
        Id = id;
        Name = name;
        Amenities = amenities;
        RoomTypes = roomTypes;
    }
}


