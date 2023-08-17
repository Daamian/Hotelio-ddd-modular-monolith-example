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
    private readonly Status[] ActiveStatuses = { Status.CREATED, Status.CONFIRMED, Status.STARTED };
    private readonly Status[] AcceptedStatuses = { Status.CONFIRMED, Status.STARTED };

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
        if (price > (this.PriceToPay - this.PricePayed))
        {
            throw new DomainException("Cannot pay with higher price.");
        }

        if (IsPaid())
        {
            throw new DomainException("Reservation is payed yet.");
        }

        if (!this.IsActive())
        {
            throw new DomainException("Cannot pay not active reservation");
        }

        this.PricePayed += price;
    }

    public void Confirm()
    {
        if (!this.IsPaid() && this.PaymentType == PaymentType.IN_ADVANCE)
        {
            throw new DomainException("Reservation must be paid to confirm");
        }

        if (this.Status != Status.CREATED)
        {
            throw new DomainException("Cannot confirm reservation on this status");
        }

        this.Status= Status.CONFIRMED;
    }

    public void AddAmenity(Amenity amenity, HotelConfig hotel)
    {
        if (!this.IsAccepted())
        {
            throw new DomainException("Cannot modify not accepted reservation");
        }

        if (!IsAmenitiesAvailableInHotel(new List<Amenity> { amenity }, hotel ))
        {
            throw new DomainException($"Amentiy {amenity.Id} not exist in hotel {hotel.Id}");
        }

        if (IsAmenityExists(amenity))
        {
            throw new DomainException("Invalid amenities");
        }

        this.Amenities.Add(amenity);
    }

    public void ChangeRoomType(int roomType, HotelConfig hotelConfig)
    {
        //To consider isRoomAvailable() ???
        if (!IsAccepted())
        {
            throw new DomainException("Cannot modify not accepted reservation");
        }

        var roomTypeConfig = hotelConfig.roomTypes.Find(r => r.RoomType == roomType);

        if (null == roomTypeConfig)
        {
            throw new DomainException($"Invalid room type {roomType} for hotel {hotelConfig.Id}");
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
        if (!IsAccepted())
        {
            throw new DomainException("Cannot modify not accepted reservation");
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

        throw new NotImplementedException();
    }

    public void Start()
    {
        if (this.Status == Status.STARTED)
        {
            throw new DomainException("Reservation has already started.");
        }

        if (!IsActive())
        {
            throw new DomainException("Cannot start not active reservation.");
        }

        this.Status = Status.STARTED;
    }

    public void Finish()
    {
        if (this.Status != Status.STARTED)
        {
            throw new DomainException("Cannot finish not started reservation.");
        }

        if (!IsPaid())
        {
            throw new DomainException("Cannot finish not paid reservation.");
        }

        this.Status = Status.FINISHED;
    }

    public void Cancel()
    {
        if (this.Status == Status.STARTED)
        {
            throw new DomainException("Cannot cancel started reservation.");
        }

        if (this.Status == Status.FINISHED)
        {
            throw new DomainException("Cannot cancel finished reservation.");
        }

        this.Status = Status.CANCELED;
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
        if (this.ActiveStatuses.Contains(this.Status))
        {
            return true;
        }

        return false;
    }

    private bool IsAccepted()
    {
        if (this.AcceptedStatuses.Contains(this.Status))
        {
            return true;
        }

        return false;
    }

    private bool IsAmenityExists(Amenity amenity)
    {
        return this.Amenities.Contains(amenity);
    }

    private bool IsRoomTypHigherThanBefore(int roomType, HotelConfig hotelConfig)
    {
        return true;
    } 

    private bool IsPaid()
    {
        return this.PricePayed == this.PriceToPay;
    }
}

