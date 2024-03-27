namespace Hotelio.Modules.HotelManagement.Core.Service.Exception;

internal class RoomNotFoundException : System.Exception
{
    public RoomNotFoundException(string message) : base(message)
    {
    }
}