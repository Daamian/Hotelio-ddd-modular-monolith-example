namespace Hotelio.CrossContext.Contract.HotelManagement.DTO;

public class RoomType
{
    public readonly int Id;
    public readonly int MaxGuests;
    public readonly int Level;

    public RoomType(int id, int maxGuests, int level)
    {
        this.Id = id;
        this.MaxGuests = maxGuests;
        this.Level = level;
    }
}


