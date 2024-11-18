namespace Hotelio.Modules.Pricing.Domain.Model;

internal class HotelTariff
{
    public static HotelTariff Create(string hotelId, double basePrice)
    {
        throw new NotImplementedException();
    }

    public void AddRoomTariff(RoomTariff roomTariff)
    {
        throw new NotImplementedException();
    }

    public void AddAmenityTariff(AmenityTariff amenityTariff)
    {
        throw new NotImplementedException();
    }

    public double Calculate(string roomType, List<string> amenities, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }
}