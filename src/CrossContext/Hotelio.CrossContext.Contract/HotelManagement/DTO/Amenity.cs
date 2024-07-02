namespace Hotelio.CrossContext.Contract.HotelManagement.DTO;

public class Amenity
{
    public readonly string Id;
    public readonly string Name;

    public Amenity(string id, string name)
    {
        Id = id;
        Name = name;
    }
}


