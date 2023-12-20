namespace Hotelio.CrossContext.Contract.HotelManagement.Exception;

using System;

public class HotelNotFoundException : Exception
{
    public HotelNotFoundException(string message) : base(message)
    {
    }
}


