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
        if (IsPaid())
        {
            throw new DomainException("Reservation is payed yet.");
        }

        this.PricePayed += price;
    }

    public void Confirm()
    {
        if (this.PaymentType == PaymentType.IN_ADVANCE)
        {
            throw new DomainException("Reservation must be paid to confirm");
        }

        this.Status= Status.CONFIRMED;
    }

    public void AddAmenity(Amenity amenity, HotelConfig hotel)
    {
        if (this.Status != Status.CONFIRMED)
        {
            throw new DomainException("Cannot modify not confirmed reservation");
        }

        if (!IsAmenitiesAvailableInHotel(new List<Amenity> { amenity }, hotel ))
        {
            throw new DomainException($"Amentiy {amenity.Id} not exist in hotel {hotel.Id}");
        }

        if (IsAmenityNotExists(amenity))
        {
            throw new Exception("Invalid amenities");
        }

        this.Amenities.Add(amenity);
    }

    public void ChangeRoomType(int roomType, HotelConfig hotelConfig)
    {
        //To consider isRoomAvailable() ???
        if (this.Status != Status.CONFIRMED)
        {
            throw new DomainException("Cannot modify not confirmed reservation");
        }

        //polityka ???
        if (!IsRoomTypHigherThanBefore(roomType, hotelConfig))
        {
            throw new DomainException("Cannot change room type to lower");
        }

        this.RoomType = roomType;
    }

    public void ChangeNumberOfGuests(int numberOfGuests, RoomTypeConfig roomTypeConfig)
    {
        if (this.Status != Status.CONFIRMED)
        {
            throw new DomainException("Cannot modify not confirmed reservation");
        }

        if (!IsNumberOfGuestCorrectToRoomType(numberOfGuests, roomTypeConfig))
        {
            throw new DomainException("Invalid number of guest for room type");
        }

        if (numberOfGuests <= this.NumberOfGuests)
        {
            throw new DomainException("Cannot change number of guests to lower number");
        }

        this.NumberOfGuests = numberOfGuests;
    }

    public void Extend(DateRange dateRange)
    {
        //isRoomAvailable() ???

        if (this.Status != Status.CONFIRMED)
        {
            throw new DomainException("Cannot modify not confirmed reservation");
        }

        if (!dateRange.isGratherThan(this.DateRange))
        {
            throw new DomainException("Cannot change reservation to less than current");
        }

        this.DateRange = dateRange;
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

    private bool IsAmenityNotExists(Amenity amenity)
    {
        return this.Amenities.Contains(amenity);
    }

    private bool IsConfirmed()
    {
        throw new NotImplementedException();
    }

    private bool IsRoomTypHigherThanBefore(int roomType, HotelConfig hotelConfig)
    {
        return true;
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
        return this.PricePayed == this.PriceToPay;
    }
}

