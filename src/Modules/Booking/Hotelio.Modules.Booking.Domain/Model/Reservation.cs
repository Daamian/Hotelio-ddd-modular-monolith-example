using Hotelio.Modules.Booking.Domain.Event;
using Hotelio.Modules.Booking.Domain.Model.DTO;
using Hotelio.Shared.Exception;
using Hotelio.Modules.Booking.Domain.Exception;
using Hotelio.Shared.Domain;

namespace Hotelio.Modules.Booking.Domain.Model;

internal class Reservation: Aggregate
{
    public string Id { get; private set; }
    public string HotelId { get; private set; }
    public string OwnerId { get; private set; }
    public int RoomType { get; private set; }
    public string? RoomId { get; private set; } = null;
    public int NumberOfGuests { get; private set; }
    public Status Status { get; private set; }
    public double PriceToPay { get; private set; }
    public double PricePayed { get; private set; }
    public PaymentType PaymentType { get; private set; }
    public DateRange DateRange { get; private set; }
    private List<Amenity> _amenities;
    public IReadOnlyList<Amenity> Amenities => _amenities.AsReadOnly();
    
    private readonly Status[] _activeStatuses = { Status.Created, Status.Confirmed, Status.Started };
    private readonly Status[] _acceptedStatuses = { Status.Confirmed, Status.Started };
    
    protected Reservation() {}

    private Reservation(string id, string hotelId, string ownerId, int roomType, int numberOfGuests, Status status, double priceToPay, double pricePayed, PaymentType paymentType, DateRange dateRange, List<Amenity> amenities)
    {
        Id = id;
        HotelId = hotelId;
        OwnerId = ownerId;
        RoomType = roomType;
        NumberOfGuests = numberOfGuests;
        Status = status;
        PriceToPay = priceToPay;
        PricePayed = pricePayed;
        PaymentType = paymentType;
        DateRange = dateRange;
        _amenities = amenities;
    }

    public static Reservation Create(string id, HotelConfig hotel, string ownerId, int roomType, int numberOfGuests, double priceToPay, PaymentType paymentType, DateRange dateRange, List<Amenity> amenities)
    {
        var roomTypeConfig = hotel.RoomTypes.Find(r => r.RoomType == roomType);

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
            throw new DomainException("Invalid amenities for hotel");
        }

        var reservation = new Reservation(id, hotel.Id,  ownerId, roomType, numberOfGuests, Status.Created, priceToPay, 0, paymentType, dateRange, amenities);
        
        reservation.Events.Add(new ReservationCreated(id));
        
        return reservation;
    }

    public string GetId()
    {
        return Id;
    }

    public void Pay(double price)
    {
        if (price > (PriceToPay - PricePayed))
        {
            throw new DomainException("Cannot pay with higher price.");
        }

        if (IsPaid())
        {
            throw new DomainException("Reservation is payed yet.");
        }

        if (!IsActive())
        {
            throw new DomainException("Cannot pay not active reservation");
        }

        PricePayed += price;

        if (IsPaid())
        {
            Events.Add(new ReservationPayed(Id));
        }
    }

    public void Confirm(string roomId)
    {
        if (!IsPaid() && PaymentType == PaymentType.InAdvance)
        {
            throw new DomainException("Reservation must be paid to confirm");
        }

        if (Status != Status.Created)
        {
            throw new DomainException("Cannot confirm reservation on this status");
        }

        Status= Status.Confirmed;
        RoomId = roomId;
        Events.Add(new ReservationConfirmed(Id));
    }

    public void AddAmenity(Amenity amenity, HotelConfig hotel)
    {
        if (!IsAccepted())
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

        _amenities.Add(amenity);
    }

    public void ChangeRoomType(int roomType, HotelConfig hotelConfig)
    {
        if (!IsAccepted())
        {
            throw new DomainException("Cannot modify not accepted reservation");
        }

        var roomTypeConfig = hotelConfig.RoomTypes.Find(r => r.RoomType == roomType);

        if (null == roomTypeConfig)
        {
            throw new DomainException($"Invalid room type {roomType} for hotel {hotelConfig.Id}");
        }

        if (!IsRoomTypHigherThanBefore(roomType, hotelConfig))
        {
            throw new ChangeToLowerRoomTypeNotAllowedException($"Room type {roomType} has lowest level than current");
        }

        RoomType = roomType;
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

        if (numberOfGuests <= NumberOfGuests)
        {
            throw new DomainException("Cannot change number of guests to lower number");
        }

        NumberOfGuests = numberOfGuests;
    }

    public void Extend(DateRange dateRange)
    {
        if (!IsAccepted())
        {
            throw new CannotModifyNotAcceptedReservationException("Cannot extend not accepted reservation");
        }

        if (!dateRange.IsGratherThan(DateRange))
        {
            throw new InvalidDateRangeToExtendReservationException("Cannot change reservation to less than current");
        }

        DateRange = dateRange;
    }

    public void Start()
    {
        if (Status == Status.Started)
        {
            throw new ReservationAlreadyStartedException("Reservation has already started.");
        }

        if (Status != Status.Confirmed)
        {
            throw new CannotStartNonConfirmedReservationException("Cannot start non confirmed reservation.");
        }

        Status = Status.Started;
    }

    public void Reject()
    {
        Status = Status.Rejected;
        Events.Add(new ReservationRejected(Id));
    }

    public void Finish()
    {
        if (Status != Status.Started)
        {
            throw new CannotFinishNotStartedReservationException("Cannot finish not started reservation.");
        }

        if (!IsPaid())
        {
            throw new NotPaidReservationException("Cannot finish not paid reservation.");
        }

        Status = Status.Finished;
    }

    public void Cancel()
    {
        Status = Status switch
        {
            Status.Started => throw new CannotCancelStartedReservationException("Cannot cancel started reservation."),
            Status.Finished =>
                throw new CannotCancelFinishedReservationException("Cannot cancel finished reservation."),
            _ => Status.Canceled
        };
    }

    private static bool IsAmenitiesAvailableInHotel(List<Amenity> amenities, HotelConfig hotel)
    {
        return amenities.Select(amenity => hotel.Amenities.Find(a => a == amenity.Id)).All(amenityFound => null != amenityFound);
    }

    private static bool IsNumberOfGuestCorrectToRoomType(int numberOfGuests, RoomTypeConfig roomTypeConfig)
    {
        return numberOfGuests <= roomTypeConfig.MaxGuests;
    }

    private bool IsActive()
    {
        return _activeStatuses.Contains(Status);
    }

    private bool IsAccepted()
    {
        return _acceptedStatuses.Contains(Status);
    }

    private bool IsAmenityExists(Amenity amenity)
    {
        return _amenities.Contains(amenity);
    }

    private bool IsRoomTypHigherThanBefore(int roomType, HotelConfig hotelConfig)
    {
        var roomTypes = hotelConfig.RoomTypes;
        var newRoomType = roomTypes.Find(r => r.RoomType == roomType);

        var currentRoomType = roomTypes.Find(r => r.RoomType == RoomType);

        return null != newRoomType && null != currentRoomType && newRoomType.Level >= currentRoomType.Level;
    } 

    private bool IsPaid()
    {
        return PricePayed == PriceToPay;
    }
}

