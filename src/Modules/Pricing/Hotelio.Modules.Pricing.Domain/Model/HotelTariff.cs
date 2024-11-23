using System.Data.Common;
using Hotelio.Shared.Domain;
using Hotelio.Shared.Exception;

namespace Hotelio.Modules.Pricing.Domain.Model;

internal class HotelTariff : Aggregate
{
    public Guid Id { get; private set; }
    public string HotelId { get; private set; }
    public Price BasePrice { get; private set; }
    private readonly List<RoomTariff> _roomTariffs = new List<RoomTariff>();
    public IReadOnlyList<RoomTariff> RoomTariffs => _roomTariffs.AsReadOnly();
    
    private readonly List<AmenityTariff> _amenityTariffs = new List<AmenityTariff>();
    public IReadOnlyList<AmenityTariff> AmenityTariffs => _amenityTariffs.AsReadOnly();

    private HotelTariff(Guid id, string hotelId, Price basePrice)
    {
        Id = id;
        HotelId = hotelId;
        BasePrice = basePrice;
    }

    public static HotelTariff Create(string hotelId, Price basePrice)
    {
        return new HotelTariff(Guid.NewGuid(), hotelId, basePrice);
    }

    public void AddRoomTariff(RoomTariff roomTariff)
    {
        if (_roomTariffs.Any(rt => rt.RoomTypeId == roomTariff.RoomTypeId))
            throw new DomainException("Room tariff for this type already exists.");
        _roomTariffs.Add(roomTariff);
    }

    public void AddAmenityTariff(AmenityTariff amenityTariff)
    {
        if (_amenityTariffs.Any(at => at.AmenityId == amenityTariff.AmenityId))
            throw new DomainException("Amenity tariff for this type already exists.");
        _amenityTariffs.Add(amenityTariff);
    }

    public double Calculate(string roomType, List<string> amenities, DateTime startDate, DateTime endDate)
    {
        var roomTariff = _roomTariffs.FirstOrDefault(rt => rt.RoomTypeId == roomType);
        var roomPrice = roomTariff == null
            ? BasePrice
            : roomTariff.GetPriceForPeriod(startDate, endDate) ?? roomTariff.BasePrice; // Jeśli brak ceny w RoomTariff, użyj BasePrice z RoomTariff
        
        var amenityCost = amenities
            .Select(amenityId =>
                _amenityTariffs.FirstOrDefault(at => at.AmenityId == amenityId)?.Price.NetAmount ?? 0)
            .Sum();
        
        return roomPrice.NetAmount + amenityCost;
    }
}