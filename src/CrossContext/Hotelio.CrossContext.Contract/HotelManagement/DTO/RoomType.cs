namespace Hotelio.CrossContext.Contract.HotelManagement.DTO;

public class RoomType
{
    public readonly int Id;
    public readonly string Name;
    public readonly int MaxGuests;
    public readonly int Level;

    public RoomType(int id, string name, int maxGuests, int level)
    {
        Id = id;
        Name = name;
        MaxGuests = maxGuests;
        Level = level;
    }
}


