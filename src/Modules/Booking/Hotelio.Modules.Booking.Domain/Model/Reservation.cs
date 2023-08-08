using Hotelio.Modules.Booking.Domain.Model.DTO;
using Hotelio.Shared.Exception;

namespace Hotelio.Modules.Booking.Domain.Model;

internal class Reservation
{
    private string Id;
    private string HotelId;
    private int RoomType;
    private int NumberOfGuests;
    private Status Status;
    private double PriceToPay;
    private double PricePayed;
    private PaymentType PaymentType;
    private DateRange DateRange;
    private List<Amenity> Amenities;

    private Reservation(string id, string hotelId, int roomType, int numberOfGuests, Status status, double priceToPay, double pricePayed, PaymentType paymentType, DateRange dateRange, List<Amenity> amenities)
    {
        Id = id;
        HotelId = hotelId;
        RoomType = roomType;
        NumberOfGuests = numberOfGuests;
        Status = status;
        PriceToPay = priceToPay;
        PricePayed = pricePayed;
        PaymentType = paymentType;
        DateRange = dateRange;
        Amenities = amenities;
    }

    public static Reservation Create(string id, HotelConfig hotel, int roomType, int numberOfGuests, double priceToPay, PaymentType paymentType, DateRange dateRange, List<Amenity> amenities)
    {
        var roomTypeConfig = hotel.roomTypes.Find(r => r.RoomType == roomType);

        if (null == roomTypeConfig)
        {
            throw new DomainException($"Invalid room type {roomType} for hotel {hotel.Id}");
        }

        if (!IsNumberOfGuestCorrectToRoomType(numberOfGuests, roomTypeConfig))
        {
            throw new DomainException($"Room type {roomTypeConfig.RoomType} has only {roomTypeConfig.MaxGuests} guests");
        }
        
        if (!IsAmenitiesAvailableInHotel(amenities, hotel))
        {
            throw new DomainException($"Invalid amenities for hotel");
        }

        return new Reservation(id, hotel.Id, roomType, numberOfGuests, Status.CREATED, priceToPay, 0, paymentType, dateRange, amenities);
    }

    public void Pay(double price)
    {
        if (!IsUnpaid() && IsActive())
        {
            throw new Exception("Invalid state");
        }

        throw new NotImplementedException();
    }

    public void Confirm()
    {
        if (!IsPaidIfPaymentInAdvance())
        {
            throw new Exception("Invalid state");
        }

        throw new NotImplementedException();
    }

    public void AddAmenity(string amenity)
    {
        if (!IsConfirmed())
        {
            throw new Exception("Invalid amenities");
        }

        /*if (!IsAmenitiesAvailableInHotel())
        {
            throw new Exception("Invalid amenities");
        }

        if (IsAmenityNotExists(amenity))
        {
            throw new Exception("Invalid amenities");
        }*/

        throw new NotImplementedException();
    }

    public void ChangeRoomType(int roomType, HotelConfig hotelConfig)
    {
        //To consider isRoomAvailable() ???

        if (!IsConfirmed())
        {
            throw new Exception("Invalid amenities");
        }

        //polityka ???
        if (!IsRoomTypHigherThanBefore(roomType, hotelConfig))
        {
            throw new Exception("Invalid amenities");
        }

        throw new NotImplementedException();
    }

    public void ChangeNumberOfGuests(int numberOfGuests, RoomTypeConfig roomTypeConfig)
    {
        if (!IsConfirmed())
        {
            throw new Exception();
        }

        /*if (IsNumberOfGuestCorrectToRoomType())
        {
            throw new Exception();
        }*/

        if (numberOfGuests <= this.NumberOfGuests)
        {
            throw new Exception();
        }

        throw new NotImplementedException();
    }

    public void Extend(DateTime startDate, DateTime endDate)
    {
        //isRoomAvailable() ???

        if (!IsConfirmed())
        {
            throw new Exception();
        }

        if (!IsDateRangeGratherThanCurrentRange(startDate, endDate))
        {
            throw new Exception();
        }

        throw new NotImplementedException();
    }

    public void ForwardToService()
    {
        //TODO consider is nessecary
        if (!IsStarted() || !IsActive())
        {
            throw new Exception();
        }

        throw new NotImplementedException();
    }

    public void Start()
    {
        if (!IsStarted() || !IsActive() || IsCurrentDateEqualToOrGratherThanStarDate())
        {
            throw new Exception();
        }

        throw new NotImplementedException();
    }

    public void Finish()
    {
        if (!IsStarted() || !IsPaid()) { 
            throw new Exception(); 
        }

        throw new NotImplementedException();
    }

    public void Cancel()
    {
        if (IsPending())
        {
            throw new Exception("Invalid status");
        }

        throw new NotImplementedException();
    }

    public IDictionary<string, object> Snapshot()
    {
        return new Dictionary<string, object>
        {
            { "Id", this.Id },
            { "HotelId", this.HotelId },
            { "RoomType", this.RoomType },
            { "NumberOfGuests", this.NumberOfGuests },
            { "Status", this.Status },
            { "PriceToPay", this.PriceToPay },
            { "PricePayed", this.PricePayed },
            { "PaymentType", this.PaymentType },
            { "DateRange", this.DateRange },
            { "Amenities", this.Amenities },
        };
    }

    private bool IsPending()
    {
        throw new NotImplementedException();
    }

    private static bool IsAmenitiesAvailableInHotel(List<Amenity> amenities, HotelConfig hotel)
    {
        foreach (var amenity in amenities) {
            var amenityFound = hotel.amenities.Find(a => a == amenity.Id);
            if (null == amenityFound) { return false; }
        }

        return true;
    }

    private static bool IsNumberOfGuestCorrectToRoomType(int numberOfGuests, RoomTypeConfig roomTypeConfig)
    {
        if (numberOfGuests > roomTypeConfig.MaxGuests)
        {
            return false;
        }

        return true;
    }

    private bool IsActive()
    {
        throw new NotImplementedException();
    }

    private bool IsUnpaid()
    {
        throw new NotImplementedException();
    }

    private bool IsPaidIfPaymentInAdvance()
    {
        throw new NotImplementedException();
    }

    private bool IsAmenityNotExists(int amenity)
    {
        throw new NotImplementedException();
    }

    private bool IsConfirmed()
    {
        throw new NotImplementedException();
    }

    private bool IsRoomTypHigherThanBefore(int roomType, HotelConfig hotelConfig)
    {
        throw new NotImplementedException();
    }

    private bool IsDateRangeGratherThanCurrentRange(DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }

    private bool IsStarted()
    {
        throw new NotImplementedException();
    }

    private bool IsCurrentDateEqualToOrGratherThanStarDate()
    {
        throw new NotImplementedException();
    }

    private bool IsPaid()
    {
        throw new NotImplementedException();
    }
}

