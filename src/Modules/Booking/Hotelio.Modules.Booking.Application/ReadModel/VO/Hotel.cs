using System;

namespace Hotelio.Modules.Booking.Application.ReadModel.VO;

internal class Hotel
{
    public Guid Id { set; get; }
    public String Name { set; get; } = "";

    public Hotel(Guid Id, String Name)
    {
        this.Id = Id;
        this.Name = Name;
    }
}


