namespace Hotelio.Modules.HotelManagement.Core.Service.Exception;

internal class HotelNotFoundException : System.Exception
{
    public HotelNotFoundException(string message) : base(message)
    {
    }
}