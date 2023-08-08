using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Model.DTO;

namespace Hotelio.Modules.Booking.Test.Unit.Domain.Model;

public class ReservationTest
{
    [Fact]
    public void TestShouldCreateReservation()
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

    [Fact]
    public void TestShouldConfirmReservation()
    {
        //Given
        var reservation = this.CreteReservation();

        //Expected
        var snapshotExpected = reservation.Snapshot();
        snapshotExpected["Status"] = Status.CONFIRMED;

        //When
        reservation.Confirm();

        //Then
        Assert.Equal(snapshotExpected, reservation.Snapshot());
    }

    [Fact]
    public void TestShouldPayReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        double price = 200.0;

        //Expected
        var snapshotExpected = reservation.Snapshot();
        snapshotExpected["PricePayed"] = 200.0;

        //When
        reservation.Pay(price);

        //Then
        Assert.Equal(snapshotExpected, reservation.Snapshot());
    }

    [Fact]
    public void TestShouldAddAmenity()
    {
        //Given
        var reservation = this.CreteReservation();
        var amenity = new Amenity("amenity-test-1");
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1"}, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2) });

        //Expected
        var snapshotExpected = reservation.Snapshot();
        snapshotExpected["Amenities"] = new List<Amenity> { amenity };
        snapshotExpected["Status"] = Status.CONFIRMED;

        //When
        reservation.Confirm();
        reservation.AddAmenity(amenity, hotelConfig);

        //Then
        Assert.Equal(snapshotExpected, reservation.Snapshot());
    }

    [Fact]
    public void TestShouldChangeRoomType()
    {
        //Given
        var reservation = this.CreteReservation();
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1" }, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2) });

        //Expected
        var snapshotExpected = reservation.Snapshot();
        snapshotExpected["RoomType"] = 2;
        snapshotExpected["Status"] = Status.CONFIRMED;

        //When
        reservation.Confirm();
        reservation.ChangeRoomType(2, hotelConfig);

        //Then
        Assert.Equal(snapshotExpected, reservation.Snapshot());
    }

    [Fact]
    public void TestShouldChangeNumberOfGuests()
    {
        //Given
        var reservation = this.CreteReservation();

        //Expected
        var snapshotExpected = reservation.Snapshot();
        snapshotExpected["NumberOfGuests"] = 3;
        snapshotExpected["Status"] = Status.CONFIRMED;

        //When
        reservation.Confirm();
        reservation.ChangeNumberOfGuests(3, new RoomTypeConfig(1, 3));

        //Then
        Assert.Equal(snapshotExpected, reservation.Snapshot());
    }

    private Reservation CreteReservation()
    {
        var id = Guid.NewGuid().ToString();
        var roomType = 1;
        var numberOfGuests = 2;
        var priceToPay = 100.0;
        var paymentType = PaymentType.POST_PAID;
        DateTime currentDate = DateTime.Now;
        DateTime futureDate = currentDate.AddDays(7);
        var dateRange = new DateRange(currentDate, futureDate);
        var amenities = new List<Amenity>();
        var hotelConfig = new HotelConfig("hotel-1", new List<string>(), new List<RoomTypeConfig> { new RoomTypeConfig(1, 2) });

        return Reservation.Create(id, hotelConfig, roomType, numberOfGuests, priceToPay, paymentType, dateRange, amenities);
    }
}

