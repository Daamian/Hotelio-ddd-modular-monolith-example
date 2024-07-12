using Hotelio.Shared.Exception;

namespace Hotelio.Modules.Booking.Domain.Exception;

public class CannotModifyNotAcceptedReservationException : DomainException
{ 
    public CannotModifyNotAcceptedReservationException(string message) : base(message)
    {
    }
}


