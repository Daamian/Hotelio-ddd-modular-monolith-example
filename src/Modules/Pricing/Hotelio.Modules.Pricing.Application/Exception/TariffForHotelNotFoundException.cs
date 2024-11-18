namespace Hotelio.Modules.Pricing.Application.Exception;

internal class TariffForHotelNotFoundException: System.Exception
{
    public TariffForHotelNotFoundException(string message) : base(message)
    {
    }
}