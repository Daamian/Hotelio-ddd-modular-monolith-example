using Hotelio.Shared.Exception;
namespace Hotelio.Modules.Booking.Domain.Exception;

public class NotPaidReservationException : DomainException
{
	public NotPaidReservationException(string message) : base(message)
    {
	}
}


