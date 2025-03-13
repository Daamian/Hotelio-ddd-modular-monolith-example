using Hotelio.Shared.Domain;
using Hotelio.Shared.Exception;

namespace Hotelio.Modules.Pricing.Test.Unit.Domain;

using Hotelio.Modules.Pricing.Domain.Model;

public class HotelTariffTest
{
[Fact]
    public void TestCreateHotelTariff()
    {
        // Arrange
        var hotelId = "Hotel123";
        var basePrice = Price.Create(200, Currency.PLN, 8);

        // Act
        var hotelTariff = HotelTariff.Create(hotelId, basePrice);

        // Assert
        Assert.NotNull(hotelTariff);
        Assert.Equal(hotelId, hotelTariff.HotelId);
        Assert.Equal(basePrice, hotelTariff.BasePrice);
        Assert.Empty(hotelTariff.RoomTariffs);
    }

    [Fact]
    public void TestAddRoomTariff()
    {
        // Arrange
        var hotelTariff = HotelTariff.Create("Hotel123", Price.Create(200, Currency.PLN, 8));
        var roomTariff = RoomTariff.Create("Deluxe", Price.Create(150, Currency.PLN, 8));

        // Act
        hotelTariff.AddRoomTariff(roomTariff);

        // Assert
        Assert.Contains(roomTariff, hotelTariff.RoomTariffs);
    }

    [Fact]
    public void TestAddRoomTariffShouldThrowExceptionWhenRoomTypeAlreadyExists()
    {
        // Arrange
        var hotelTariff = HotelTariff.Create("Hotel123", Price.Create(200, Currency.PLN, 8));
        var roomTariff = RoomTariff.Create("Deluxe", Price.Create(150, Currency.PLN, 8));
        hotelTariff.AddRoomTariff(roomTariff);

        // Act & Assert
        Assert.Throws<DomainException>(() => hotelTariff.AddRoomTariff(roomTariff));
    }

    [Fact]
    public void TestAddAmenityTariff()
    {
        // Arrange
        var hotelTariff = HotelTariff.Create("Hotel123", Price.Create(200, Currency.PLN, 8));
        var amenityTariff = AmenityTariff.Create("Breakfast", Price.Create(50, Currency.PLN, 8));

        // Act
        hotelTariff.AddAmenityTariff(amenityTariff);

        // Assert
        Assert.Contains(amenityTariff, hotelTariff.AmenityTariffs);
    }

    [Fact]
    public void TestAddAmenityTariffShouldThrowExceptionWhenAmenityAlreadyExists()
    {
        // Arrange
        var hotelTariff = HotelTariff.Create("Hotel123", Price.Create(200, Currency.PLN, 8));
        var amenityTariff = AmenityTariff.Create("Breakfast", Price.Create(50, Currency.PLN, 8));
        hotelTariff.AddAmenityTariff(amenityTariff);

        // Act & Assert
        Assert.Throws<DomainException>(() => hotelTariff.AddAmenityTariff(amenityTariff));
    }

    [Fact]
    public void TestCalculateShouldUseBasePriceWhenRoomTypeNotFound()
    {
        // Arrange
        var hotelTariff = HotelTariff.Create("Hotel123", Price.Create(200, Currency.PLN, 8));

        // Act
        var price = hotelTariff.Calculate("NonExistentRoom", new List<string>(), DateTime.Now, DateTime.Now.AddDays(1));

        // Assert
        Assert.Equal(200, price);
    }

    [Fact]
    public void TestCalculateShouldUseRoomBasePriceWhenPeriodNotFound()
    {
        // Arrange
        var hotelTariff = HotelTariff.Create("Hotel123", Price.Create(200, Currency.PLN, 8));
        var roomTariff = RoomTariff.Create("Deluxe", Price.Create(150, Currency.PLN, 8));
        hotelTariff.AddRoomTariff(roomTariff);

        // Act
        var price = hotelTariff.Calculate("Deluxe", new List<string>(), DateTime.Now, DateTime.Now.AddDays(1));

        // Assert
        Assert.Equal(150, price);
    }

    [Fact]
    public void TestCalculateShouldUsePeriodPriceWhenWithinPeriod()
    {
        // Arrange
        var hotelTariff = HotelTariff.Create("Hotel123", Price.Create(200, Currency.PLN, 8));
        var roomTariff = RoomTariff.Create("Deluxe", Price.Create(150, Currency.PLN, 8));
        roomTariff.AddPeriodPrice(Price.Create(180, Currency.PLN, 8), new Period(DateTime.Now, DateTime.Now.AddDays(5)));
        hotelTariff.AddRoomTariff(roomTariff);

        // Act
        var price = hotelTariff.Calculate("Deluxe", new List<string>(), DateTime.Now, DateTime.Now.AddDays(1));

        // Assert
        Assert.Equal(180, price);
    }

    [Fact]
    public void TestCalculateShouldIncludeAmenityCostWhenAmenitiesAreProvided()
    {
        // Arrange
        var hotelTariff = HotelTariff.Create("Hotel123", Price.Create(200, Currency.PLN, 8));
        var amenityTariff = AmenityTariff.Create("Breakfast", Price.Create(50, Currency.PLN, 8));
        hotelTariff.AddAmenityTariff(amenityTariff);

        // Act
        var price = hotelTariff.Calculate("NonExistentRoom", new List<string> { "Breakfast" }, DateTime.Now, DateTime.Now.AddDays(1));

        // Assert
        Assert.Equal(250, price); // BasePrice (200) + Amenity (50)
    }
}