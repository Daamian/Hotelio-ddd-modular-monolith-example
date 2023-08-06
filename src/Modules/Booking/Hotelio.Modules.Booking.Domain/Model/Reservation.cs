using Hotelio.Modules.Booking.Domain.Model.DTO;

namespace Hotelio.Modules.Booking.Domain.Model;

internal class Reservation
{
    private int HotelId;
    private int RoomType;
    private List<int> Amenities;
    private int NumberOfGuests;
    private Status Status;
    private double PriceToPay;
    private double PricePayed;
    private PaymentType PaymentType;
    private DateRange DateRange;

    public static Reservation Create()
    {
        if (!IsNumberOfGuestCorrectToRoomType())
        {
            throw new Exception("Invalid room type");
        }
        
        if (!IsAmenitiesAvailableInHotel())
        {
            throw new Exception("Invalid amenities");
        }

        throw new NotImplementedException();
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

    public void AddAmenity(int amenity)
    {
        if (!IsConfirmed())
        {
            throw new Exception("Invalid amenities");
        }

        if (!IsAmenitiesAvailableInHotel())
        {
            throw new Exception("Invalid amenities");
        }

        if (IsAmenityNotExists(amenity))
        {
            throw new Exception("Invalid amenities");
        }

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

        if (IsNumberOfGuestCorrectToRoomType())
        {
            throw new Exception();
        }

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

    private bool IsPending()
    {
        throw new NotImplementedException();
    }

    private static bool IsAmenitiesAvailableInHotel()
    {
        throw new NotImplementedException();
    }

    private static bool IsNumberOfGuestCorrectToRoomType()
    {
        throw new NotImplementedException();
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

