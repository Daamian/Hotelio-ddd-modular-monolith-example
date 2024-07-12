using Hotelio.Shared.Exception;
namespace Hotelio.Modules.Booking.Domain.Exception;

public class CannotStartNonConfirmedReservationException : DomainException
{
	public CannotStartNonConfirmedReservationException(string message) : base(message)
    {
	}
}


