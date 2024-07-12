using Hotelio.Shared.Exception;
namespace Hotelio.Modules.Booking.Domain.Exception;

public class CannotFinishNotStartedReservationException : DomainException
{
	public CannotFinishNotStartedReservationException(string message) : base(message)
    {
	}
}


