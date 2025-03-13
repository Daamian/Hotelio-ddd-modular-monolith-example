namespace Hotelio.Modules.Pricing.Application.Exception;

internal class HotelTariffNotFoundException : System.Exception
{
    public HotelTariffNotFoundException(string message) : base(message)
    {
    }
}