using System;

namespace Hotelio.Modules.Booking.Application.ReadModel.VO;

internal class Hotel
{
    public string Id { set; get; }
    public string Name { set; get; } = "";

    public Hotel(string id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
}


