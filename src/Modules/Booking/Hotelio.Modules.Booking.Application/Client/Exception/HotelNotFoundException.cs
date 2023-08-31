namespace Hotelio.Modules.Booking.Application.Client.Exception;

using System;

public class HotelNotFoundException : Exception
{
    public HotelNotFoundException(string message) : base(message)
    {
    }
}


