using Hotelio.Modules.Booking.Domain.Exception;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Model.DTO;
using Hotelio.Shared.Exception;

namespace Hotelio.Modules.Booking.Test.Unit.Domain.Model;

public class ReservationTest
{
    [Fact]
    public void TestShouldCreateReservation()
    {
        //Given
        var id = Guid.NewGuid().ToString();
        var roomType = 1;
        var owner = "Owner-1";
        var numberOfGuests = 2;
        var status = Status.Created;
        var priceToPay = 100.0;
        var pricePayed = 0.0;
        var paymentType = PaymentType.PostPaid;
        DateTime currentDate = DateTime.Now;
        DateTime futureDate = currentDate.AddDays(7);
        var dateRange = new DateRange(currentDate, futureDate);
        var amenities = new List<Amenity>();
        var hotelConfig = new HotelConfig("hotel-1", new List<string>(), new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });

        //When
        var reservation = Reservation.Create(id, hotelConfig, owner, roomType, numberOfGuests, priceToPay, paymentType, dateRange, amenities);

        //Then
        Assert.Equal(id, reservation.Id);
        Assert.Equal("hotel-1", reservation.HotelId);
        Assert.Equal("Owner-1", reservation.OwnerId);
        Assert.Equal(roomType, reservation.RoomType);
        Assert.Equal(numberOfGuests, reservation.NumberOfGuests);
        Assert.Equal(status, reservation.Status);
        Assert.Equal(priceToPay, reservation.PriceToPay);
        Assert.Equal(pricePayed, pricePayed);
        Assert.Equal(paymentType, reservation.PaymentType);
        Assert.Equal(dateRange, reservation.DateRange);
        Assert.Equal(amenities, reservation.Amenities);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenRoomTypeIsNotAvailableInHotel()
    {
        //Given
        var id = Guid.NewGuid().ToString();
        var roomType = 1;
        var owner = "Owner-1";
        var numberOfGuests = 2;
        var priceToPay = 100.0;
        var paymentType = PaymentType.PostPaid;
        DateTime currentDate = DateTime.Now;
        DateTime futureDate = currentDate.AddDays(7);
        var dateRange = new DateRange(currentDate, futureDate);
        var amenities = new List<Amenity>();
        var hotelConfig = new HotelConfig("hotel-1", new List<string>(), new List<RoomTypeConfig> { new RoomTypeConfig(33, 2, 1) });

        //Expected
        Assert.Throws<DomainException>(() => Reservation.Create(id, hotelConfig, owner, roomType, numberOfGuests, priceToPay, paymentType, dateRange, amenities));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenNumberOfGuestsIsGratherThanMaxInRoomType()
    {
        //Given
        var id = Guid.NewGuid().ToString();
        var roomType = 1;
        var owner = "Owner-1";
        var numberOfGuests = 3;
        var priceToPay = 100.0;
        var paymentType = PaymentType.PostPaid;
        DateTime currentDate = DateTime.Now;
        DateTime futureDate = currentDate.AddDays(7);
        var dateRange = new DateRange(currentDate, futureDate);
        var amenities = new List<Amenity>();
        var hotelConfig = new HotelConfig("hotel-1", new List<string>(), new List<RoomTypeConfig> { new RoomTypeConfig(33, 2, 1) });

        //Expected
        Assert.Throws<DomainException>(() => Reservation.Create(id, hotelConfig, owner, roomType, numberOfGuests, priceToPay, paymentType, dateRange, amenities));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenAmenitiesIsNotAvailableInHotel()
    {
        //Given
        var id = Guid.NewGuid().ToString();
        var roomType = 1;
        var owner = "Owner-1";
        var numberOfGuests = 3;
        var priceToPay = 100.0;
        var paymentType = PaymentType.PostPaid;
        DateTime currentDate = DateTime.Now;
        DateTime futureDate = currentDate.AddDays(7);
        var dateRange = new DateRange(currentDate, futureDate);
        var amenities = new List<Amenity> { new Amenity("amenity-1") };
        var hotelConfig = new HotelConfig("hotel-1", new List<string>(), new List<RoomTypeConfig> { new RoomTypeConfig(33, 2, 1) });

        //Expected
        Assert.Throws<DomainException>(() => Reservation.Create(id, hotelConfig, owner, roomType, numberOfGuests, priceToPay, paymentType, dateRange, amenities));
    }

    [Fact]
    public void TestShouldConfirmReservation()
    {
        //Given
        var reservation = this.CreteReservation();

        //When
        reservation.Confirm("roomId");

        //Then
        Assert.Equal(Status.Confirmed, reservation.Status);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToConfirmReservationNotPaidReservationWithPaymentTypeInAdvance()
    {
        //Given
        var id = Guid.NewGuid().ToString();
        var roomType = 1;
        var owner = "Owner-1";
        var numberOfGuests = 1;
        var priceToPay = 100.0;
        var paymentType = PaymentType.InAdvance;
        DateTime currentDate = DateTime.Now;
        DateTime futureDate = currentDate.AddDays(7);
        var dateRange = new DateRange(currentDate, futureDate);
        var amenities = new List<Amenity>();
        var hotelConfig = new HotelConfig("hotel-1", new List<string>(), new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });

        var reservation = Reservation.Create(id, hotelConfig, owner, roomType, numberOfGuests, priceToPay, paymentType, dateRange, amenities);

        //Expected
        Assert.Throws<DomainException>(() => reservation.Confirm("roomId"));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToConfirmReservationOnNotCreatedStatus()
    {
        //Given
        var id = Guid.NewGuid().ToString();
        var roomType = 1;
        var owner = "Owner-1";
        var numberOfGuests = 1;
        var priceToPay = 100.0;
        var paymentType = PaymentType.PostPaid;
        DateTime currentDate = DateTime.Now;
        DateTime futureDate = currentDate.AddDays(7);
        var dateRange = new DateRange(currentDate, futureDate);
        var amenities = new List<Amenity>();
        var hotelConfig = new HotelConfig("hotel-1", new List<string>(), new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });

        var reservation = Reservation.Create(id, hotelConfig, owner, roomType, numberOfGuests, priceToPay, paymentType, dateRange, amenities);
        reservation.Confirm("roomId");

        //Expected
        Assert.Throws<DomainException>(() => reservation.Confirm("roomId"));
    }


    [Fact]
    public void TestShouldPayReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        double price = 100.0;

        //When
        reservation.Pay(price);

        //Then
        Assert.Equal(price, reservation.PricePayed);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToPayCanceledReservation()
    {
        //Given
        var reservation = this.CreteReservation();

        //When
        reservation.Cancel();

        //Expected
        Assert.Throws<DomainException>(() => reservation.Pay(150));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToPayAlreadyPayedReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        double price = 100.0;

        //When
        reservation.Pay(price);

        //Expected
        Assert.Throws<DomainException>(() => reservation.Pay(100.0));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToPayHigherPrice()
    {
        //Given
        var reservation = this.CreteReservation();
        double price = 150.0;

        //Expected
        Assert.Throws<DomainException>(() => reservation.Pay(price));
    }

    [Fact]
    public void TestShouldAddAmenity()
    {
        //Given
        var reservation = this.CreteReservation();
        var amenity = new Amenity("amenity-test-1");
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1"}, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });

        //Expected
        var amenities = new List<Amenity> { amenity };

        //When
        reservation.Confirm("roomId");
        reservation.AddAmenity(amenity, hotelConfig);

        //Then
        Assert.Equal(amenities, reservation.Amenities);
        Assert.Equal(Status.Confirmed, reservation.Status);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToAddAmenityToNonConfirmedReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        var amenity = new Amenity("amenity-test-1");
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1" }, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });

        //Expected
        Assert.Throws<DomainException>(() => reservation.AddAmenity(amenity, hotelConfig));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToAddAmenityNotAvailableInHotel()
    {
        //Given
        var reservation = this.CreteReservation();
        reservation.Confirm("roomId");
        var amenity = new Amenity("amenity-test-2");
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1" }, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });

        //Expected
        Assert.Throws<DomainException>(() => reservation.AddAmenity(amenity, hotelConfig));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryAddAmenityExistingInReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        reservation.Confirm("roomId");
        var amenity = new Amenity("amenity-test-1");
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1" }, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });
        reservation.AddAmenity(amenity, hotelConfig);

        //Expected
        Assert.Throws<DomainException>(() => reservation.AddAmenity(amenity, hotelConfig));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToAddAmenityToCanceledReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        var amenity = new Amenity("amenity-test-1");
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1" }, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });
        reservation.Confirm("roomId");
        reservation.Cancel();

        //Expected
        Assert.Throws<DomainException>(() => reservation.AddAmenity(amenity, hotelConfig));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToAddAmenityToFinishedReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        var amenity = new Amenity("amenity-test-1");
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1" }, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });
        reservation.Confirm("roomId");
        reservation.Pay(100);
        reservation.Start();
        reservation.Finish();

        //Expected
        Assert.Throws<DomainException>(() => reservation.AddAmenity(amenity, hotelConfig));
    }

    [Fact]
    public void TestShouldChangeRoomType()
    {
        //Given
        var reservation = this.CreteReservation();
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1" }, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1), new RoomTypeConfig(2, 2, 2) });

        //When
        reservation.Confirm("roomId");
        reservation.ChangeRoomType(2, hotelConfig);

        //Then
        Assert.Equal(2, reservation.RoomType);
        Assert.Equal(Status.Confirmed, reservation.Status);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToChangeRoomTypeToLowerLevel()
    {
        //Given
        var reservation = this.CreteReservation();
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1" }, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 2), new RoomTypeConfig(2, 1, 1) });
        reservation.Confirm("roomId");

        //Expected
        Assert.Throws<ChangeToLowerRoomTypeNotAllowedException>(() => reservation.ChangeRoomType(2, hotelConfig));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToChangeRoomTypeToCanceledReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1" }, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });
        reservation.Confirm("roomId");
        reservation.Cancel();

        //Expected
        Assert.Throws<DomainException>(() => reservation.ChangeRoomType(2, hotelConfig));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToChangeRoomTypeToFinishedReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1" }, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });
        reservation.Confirm("roomId");
        reservation.Pay(100);
        reservation.Start();
        reservation.Finish();

        //Expected
        Assert.Throws<DomainException>(() => reservation.ChangeRoomType(2, hotelConfig));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToChangeRoomTypeToNonConfirmedReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1" }, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });

        //Expected
        Assert.Throws<DomainException>(() => reservation.ChangeRoomType(2, hotelConfig));
    }

    [Fact]
    public void TestShouldThrowDomainExeptionWhenTryToChangeRoomToNotAvailableInHotel()
    {
        //Given
        var reservation = this.CreteReservation();
        var hotelConfig = new HotelConfig("hotel-1", new List<string> { "amenity-test-1" }, new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });
        reservation.Confirm("roomId");

        //Expected
        Assert.Throws<DomainException>(() => reservation.ChangeRoomType(111, hotelConfig));
    }

    [Fact]
    public void TestShouldChangeNumberOfGuests()
    {
        //Given
        var reservation = this.CreteReservation();

        //When
        reservation.Confirm("roomId");
        reservation.ChangeNumberOfGuests(3, new RoomTypeConfig(1, 3, 1));

        //Then
        Assert.Equal(3, reservation.NumberOfGuests);
        Assert.Equal(Status.Confirmed, reservation.Status);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToChangeNumberOfGuestsToNonConfirmedReservation()
    {
        //Given
        var reservation = this.CreteReservation();

        //Expected
        Assert.Throws<DomainException>(() => reservation.ChangeNumberOfGuests(3, new RoomTypeConfig(1, 3, 1)));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToChangeNumberOfGuestsToCanceledReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        reservation.Confirm("roomId");
        reservation.Cancel();

        //Expected
        Assert.Throws<DomainException>(() => reservation.ChangeNumberOfGuests(3, new RoomTypeConfig(1, 3, 1)));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToChangeNumberOfGuestsToFinishedReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        reservation.Confirm("roomId");
        reservation.Start();
        reservation.Pay(100);
        reservation.Finish();

        //Expected
        Assert.Throws<DomainException>(() => reservation.ChangeNumberOfGuests(3, new RoomTypeConfig(1, 3, 1)));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToChangeNumberOfGuestsToHigherThanRoomTypeAllow()
    {
        //Given
        var reservation = this.CreteReservation();
        reservation.Confirm("roomId");

        //Expected
        Assert.Throws<DomainException>(() => reservation.ChangeNumberOfGuests(5, new RoomTypeConfig(1, 4, 1)));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenTryToChangeNumberOfGuestsToLowerThanBefore()
    {
        //Given
        var reservation = this.CreteReservation();
        reservation.Confirm("roomId");

        //Expected
        Assert.Throws<DomainException>(() => reservation.ChangeNumberOfGuests(1, new RoomTypeConfig(1, 4, 1)));
    }

    [Fact]
    public void TestShouldChangeDateRange()
    {
        //Given
        var reservation = this.CreteReservation();
        DateTime currentDate = DateTime.Now.AddDays(-1);
        DateTime futureDate = DateTime.Now.AddDays(8);
        var dateRange = new DateRange(currentDate, futureDate);

        //When
        reservation.Confirm("roomId");
        reservation.Extend(dateRange);

        //Then
        Assert.Equal(dateRange, reservation.DateRange);
        Assert.Equal(Status.Confirmed, reservation.Status);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryToExtendNonConfirmedReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        DateTime currentDate = DateTime.Now.AddDays(-1);
        DateTime futureDate = DateTime.Now.AddDays(8);
        var dateRange = new DateRange(currentDate, futureDate);
        
        //Expected
        Assert.Throws<CannotModifyNotAcceptedReservationException>(() => reservation.Extend(dateRange));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryToExtendCanceledReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        DateTime currentDate = DateTime.Now.AddDays(-1);
        DateTime futureDate = DateTime.Now.AddDays(8);
        var dateRange = new DateRange(currentDate, futureDate);
        reservation.Confirm("roomId");
        reservation.Cancel();

        //Expected
        Assert.Throws<CannotModifyNotAcceptedReservationException>(() => reservation.Extend(dateRange));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryToExtendFinishedReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        DateTime currentDate = DateTime.Now.AddDays(-1);
        DateTime futureDate = DateTime.Now.AddDays(8);
        var dateRange = new DateRange(currentDate, futureDate);
        reservation.Confirm("roomId");
        reservation.Start();
        reservation.Pay(100);
        reservation.Finish();

        //Expected
        Assert.Throws<CannotModifyNotAcceptedReservationException>(() => reservation.Extend(dateRange));
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryExtendToLowerDateRange()
    {
        //Given
        var reservation = this.CreteReservation();
        DateTime currentDate = DateTime.Now.AddDays(2);
        DateTime futureDate = DateTime.Now.AddDays(5);
        var dateRange = new DateRange(currentDate, futureDate);
        reservation.Confirm("roomId");

        //Expected
        Assert.Throws<InvalidDateRangeToExtendReservationException>(() => reservation.Extend(dateRange));
    }

    [Fact]
    public void TestShouldStartReservation()
    {
        //Given
        var reservation = this.CreteReservation();

        //When
        reservation.Confirm("roomId");
        reservation.Start();

        //Then
        Assert.Equal(Status.Started, reservation.Status);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryStartAlreadyStartedReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        reservation.Confirm("roomId");
        reservation.Start();

        //Expected
        Assert.Throws<ReservationAlreadyStartedException>(() => reservation.Start());
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryStartNonConfirmedReservation()
    {
        //Given
        var reservation = this.CreteReservation();

        //Expected
        Assert.Throws<CannotStartNonConfirmedReservationException>(() => reservation.Start());
    }

    [Fact]
    public void TestShouldFinishReservation()
    {
        //Given
        var reservation = this.CreteReservation();

        //When
        reservation.Confirm("roomId");
        reservation.Pay(100.0);
        reservation.Start();
        reservation.Finish();

        //Then
        Assert.Equal(100.0, reservation.PricePayed);
        Assert.Equal(Status.Finished, reservation.Status);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryFinishNonStartedReservation()
    {
        //Given
        var reservation = this.CreteReservation();

        //Expected
        Assert.Throws<CannotFinishNotStartedReservationException>(() => reservation.Finish());
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryFinishNonPaidReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        reservation.Confirm("roomId");
        reservation.Start();

        //Expected
        Assert.Throws<NotPaidReservationException>(() => reservation.Finish());
    }

    [Fact]
    public void TestShouldCancelReservation()
    {
        //Given
        var reservation = this.CreteReservation();

        //When
        reservation.Cancel();

        //Then
        Assert.Equal(Status.Canceled, reservation.Status);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryCancelStartedReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        reservation.Confirm("roomId");
        reservation.Start();

        //Expected
        Assert.Throws<CannotCancelStartedReservationException>(() => reservation.Cancel());
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryCancelFinishedReservation()
    {
        //Given
        var reservation = this.CreteReservation();
        reservation.Confirm("roomId");
        reservation.Start();
        reservation.Pay(100);
        reservation.Finish();

        //Expected
        Assert.Throws<CannotCancelFinishedReservationException>(() => reservation.Cancel());
    }

    private Reservation CreteReservation()
    {
        var id = Guid.NewGuid().ToString();
        var roomType = 1;
        var owner = "Owner-1";
        var numberOfGuests = 2;
        var priceToPay = 100.0;
        var paymentType = PaymentType.PostPaid;
        DateTime currentDate = DateTime.Now;
        DateTime futureDate = currentDate.AddDays(7);
        var dateRange = new DateRange(currentDate, futureDate);
        var amenities = new List<Amenity>();
        var hotelConfig = new HotelConfig("hotel-1", new List<string>(), new List<RoomTypeConfig> { new RoomTypeConfig(1, 2, 1) });

        return Reservation.Create(id, hotelConfig,  owner, roomType, numberOfGuests, priceToPay, paymentType, dateRange, amenities);
    }
}

