namespace Hotelio.Modules.Pricing.Application.Exception;

internal class RoomTariffNotFoundException : System.Exception
{
    public RoomTariffNotFoundException(string message) : base(message)
    {
    }
}