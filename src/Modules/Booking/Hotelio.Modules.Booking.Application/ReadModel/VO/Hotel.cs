using System;

namespace Hotelio.Modules.Booking.Application.ReadModel.VO;

internal class Hotel
{
    public string Id { set; get; }
    public string Name { set; get; } = "";

    public Hotel(string Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }
}


