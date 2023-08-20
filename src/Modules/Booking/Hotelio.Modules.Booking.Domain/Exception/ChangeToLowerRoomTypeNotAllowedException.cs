using System;
using Hotelio.Shared.Exception;

namespace Hotelio.Modules.Booking.Domain.Exception;

public class ChangeToLowerRoomTypeNotAllowedException : DomainException
{
	public ChangeToLowerRoomTypeNotAllowedException(string message) : base(message)
    {
	}
}


