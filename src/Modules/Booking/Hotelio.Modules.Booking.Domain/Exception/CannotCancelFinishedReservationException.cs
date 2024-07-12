using Hotelio.Shared.Exception;
namespace Hotelio.Modules.Booking.Domain.Exception;

public class CannotCancelFinishedReservationException : DomainException
{
	public CannotCancelFinishedReservationException(string message) : base(message)
    {
	}
}


