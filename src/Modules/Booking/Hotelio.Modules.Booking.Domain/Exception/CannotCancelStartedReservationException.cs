using Hotelio.Shared.Exception;
namespace Hotelio.Modules.Booking.Domain.Exception;

public class CannotCancelStartedReservationException : DomainException
{
	public CannotCancelStartedReservationException(string message) : base(message)
    {
	}
}


