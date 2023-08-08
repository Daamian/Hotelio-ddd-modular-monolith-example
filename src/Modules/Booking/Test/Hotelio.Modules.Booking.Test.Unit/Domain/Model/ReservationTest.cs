using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Model.DTO;

namespace Hotelio.Modules.Booking.Test.Unit.Domain.Model;

public class ReservationTest
{
    [Fact]
    public void testShouldCreateReservation()
    {
        //Given
        var id = Guid.NewGuid().ToString();
        var roomType = 1;
        var numberOfGuests = 2;
        var status = Status.CREATED;
        var priceToPay = 100.0;
        var pricePayed = 0.0;
        var paymentType = PaymentType.POST_PAID;
        DateTime currentDate = DateTime.Now;
        DateTime futureDate = currentDate.AddDays(7);
        var dateRange = new DateRange(currentDate, futureDate);
        var amenities = new List<Amenity>();
        var hotelConfig = new HotelConfig("hotel-1", new List<string>(), new List<RoomTypeConfig> { new RoomTypeConfig(1, 2) });

        //Expected
        var expected = new Dictionary<string, object>
        {
            { "Id", id },
            { "HotelId", "hotel-1" },
            { "RoomType", roomType },
            { "NumberOfGuests", numberOfGuests },
            { "Status", status },
            { "PriceToPay", priceToPay },
            { "PricePayed", pricePayed },
            { "PaymentType", paymentType },
            { "DateRange", dateRange },
            { "Amenities", amenities },
        };

        //When
        var reservation = Reservation.Create(id, hotelConfig, roomType, numberOfGuests, priceToPay, paymentType, dateRange, amenities);

        //Then
        Assert.Equal(expected, reservation.Snapshot());
    }
}

