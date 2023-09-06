using System;

namespace Hotelio.Modules.Booking.Application.ReadModel.VO;

internal class Amenity
{
    public string Id { set; get; }
    public string Name { set; get; }

    public Amenity(string Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }
}


