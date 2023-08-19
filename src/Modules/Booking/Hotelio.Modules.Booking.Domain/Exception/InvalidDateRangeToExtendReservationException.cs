using System;
using Hotelio.Shared.Exception;
namespace Hotelio.Modules.Booking.Domain.Exception;

public class InvalidDateRangeToExtendReservationException : DomainException
{
	public InvalidDateRangeToExtendReservationException(string message) : base(message)
    {
	}
}


