using Hotelio.Shared.Exception;
namespace Hotelio.Modules.Booking.Domain.Exception;

public class ReservationAlreadyStartedException : DomainException
{
	public ReservationAlreadyStartedException(string message) : base(message)
    {
	}
}


