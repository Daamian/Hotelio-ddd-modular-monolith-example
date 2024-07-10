using Hotelio.Modules.Booking.Domain.Event;
using Hotelio.Modules.Booking.Domain.Model.DTO;
using Hotelio.Shared.Exception;
using Hotelio.Modules.Booking.Domain.Exception;
using Hotelio.Modules.Booking.Domain.Model.Snapshot;
using Hotelio.Shared.Domain;
using AmenitySnap = Hotelio.Modules.Booking.Domain.Model.Snapshot.Amenity;

namespace Hotelio.Modules.Booking.Domain.Model;

internal class Reservation: Aggregate
{
    private string _id;
    private string _hotelId;
    private string _ownerId;
    private int _roomType;
    private string? _roomId = null;
    private int _numberOfGuests;
    private Status _status;
    private double _priceToPay;
    private double _pricePayed;
    private PaymentType _paymentType;
    private DateRange _dateRange;
    private List<Amenity> _amenities;
    private readonly Status[] _activeStatuses = { Status.Created, Status.Confirmed, Status.Started };
    private readonly Status[] _acceptedStatuses = { Status.Confirmed, Status.Started };
    
    protected Reservation() {}

    private Reservation(string id, string hotelId, string ownerId, int roomType, int numberOfGuests, Status status, double priceToPay, double pricePayed, PaymentType paymentType, DateRange dateRange, List<Amenity> amenities)
    {
        _id = id;
        _hotelId = hotelId;
        _ownerId = ownerId;
        _roomType = roomType;
        _numberOfGuests = numberOfGuests;
        _status = status;
        _priceToPay = priceToPay;
        _pricePayed = pricePayed;
        _paymentType = paymentType;
        _dateRange = dateRange;
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
            throw new DomainException($"Invalid amenities for hotel");
        }

        var reservation = new Reservation(id, hotel.Id,  ownerId, roomType, numberOfGuests, Status.Created, priceToPay, 0, paymentType, dateRange, amenities);
        
        reservation.Events.Add(new ReservationCreated(id));
        
        return reservation;
    }

    public string GetId()
    {
        return _id;
    }

    public void Pay(double price)
    {
        if (price > (_priceToPay - _pricePayed))
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

        _pricePayed += price;

        if (IsPaid())
        {
            Events.Add(new ReservationPayed(_id));
        }
    }

    public void Confirm(string roomId)
    {
        if (!IsPaid() && _paymentType == PaymentType.InAdvance)
        {
            throw new DomainException("Reservation must be paid to confirm");
        }

        if (_status != Status.Created)
        {
            throw new DomainException("Cannot confirm reservation on this status");
        }

        _status= Status.Confirmed;
        _roomId = roomId;
        Events.Add(new ReservationConfirmed(_id));
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

        this._roomType = roomType;
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

        if (numberOfGuests <= _numberOfGuests)
        {
            throw new DomainException("Cannot change number of guests to lower number");
        }

        _numberOfGuests = numberOfGuests;
    }

    public void Extend(DateRange dateRange)
    {
        if (!IsAccepted())
        {
            throw new CannotModifyNotAcceptedReservationException("Cannot extend not accepted reservation");
        }

        if (!dateRange.IsGratherThan(_dateRange))
        {
            throw new InvalidDateRangeToExtendReservationException("Cannot change reservation to less than current");
        }

        _dateRange = dateRange;
    }

    public void Start()
    {
        if (_status == Status.Started)
        {
            throw new ReservationAlreadyStartedException("Reservation has already started.");
        }

        if (this._status != Status.Confirmed)
        {
            throw new CannotStartNonConfirmedReservationException("Cannot start non confirmed reservation.");
        }

        _status = Status.Started;
    }

    public void Reject()
    {
        _status = Status.Rejected;
        Events.Add(new ReservationRejected(_id));
    }

    public void Finish()
    {
        if (_status != Status.Started)
        {
            throw new CannotFinishNotStartedReservationException("Cannot finish not started reservation.");
        }

        if (!IsPaid())
        {
            throw new NotPaidReservationException("Cannot finish not paid reservation.");
        }

        this._status = Status.Finished;
    }

    public void Cancel()
    {
        _status = _status switch
        {
            Status.Started => throw new CannotCancelStartedReservationException("Cannot cancel started reservation."),
            Status.Finished =>
                throw new CannotCancelFinishedReservationException("Cannot cancel finished reservation."),
            _ => Status.Canceled
        };
    }

    public IDictionary<string, object?> Snapshot()
    {
        return new Dictionary<string, object?>
        {
            { "Id", _id },
            { "HotelId", _hotelId },
            { "OwnerId", _ownerId},
            { "RoomType", _roomType },
            { "NumberOfGuests", _numberOfGuests },
            { "Status", _status },
            { "PriceToPay", _priceToPay },
            { "PricePayed", _pricePayed },
            { "PaymentType", _paymentType },
            { "DateRange", _dateRange },
            { "Amenities", _amenities },
            { "RoomId", _roomId}
        };
    }

    public ReservationSnapshot Snap()
    {
        return new ReservationSnapshot(
            _id,
            _hotelId,
            _ownerId,
            _roomId,
            _roomType,
            _numberOfGuests,
            (int) _status,
            _priceToPay,
            _pricePayed,
            (int) _paymentType,
            _dateRange.StartDate,
            _dateRange.EndDate,
            _amenities.Select(a => new AmenitySnap(a.Id)).ToList()
        );
    }

    private static bool IsAmenitiesAvailableInHotel(List<Amenity> amenities, HotelConfig hotel)
    {
        foreach (var amenity in amenities) {
            var amenityFound = hotel.Amenities.Find(a => a == amenity.Id);
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
        return _activeStatuses.Contains(_status);
    }

    private bool IsAccepted()
    {
        if (this._acceptedStatuses.Contains(_status))
        {
            return true;
        }

        return false;
    }

    private bool IsAmenityExists(Amenity amenity)
    {
        return this._amenities.Contains(amenity);
    }

    private bool IsRoomTypHigherThanBefore(int roomType, HotelConfig hotelConfig)
    {
        var roomTypes = hotelConfig.RoomTypes;
        var newRoomType = roomTypes.Find(r => r.RoomType == roomType);

        var currentRoomType = roomTypes.Find(r => r.RoomType == _roomType);

        if (null == newRoomType || null == currentRoomType || newRoomType.Level < currentRoomType.Level)
        {
            return false;
        }

        return true;
    } 

    private bool IsPaid()
    {
        return _pricePayed == _priceToPay;
    }
}

