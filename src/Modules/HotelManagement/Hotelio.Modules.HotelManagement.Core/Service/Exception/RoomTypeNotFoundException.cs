namespace Hotelio.Modules.HotelManagement.Core.Service.Exception;

public class RoomTypeNotFoundException : System.Exception
{
    public RoomTypeNotFoundException(string message) : base(message)
    {
    }
}