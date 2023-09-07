using System;

namespace Hotelio.Modules.Booking.Application.ReadModel.VO;

internal class Amenity
{
    public string Id { set; get; }
    public string Name { set; get; }

    public Amenity(string id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
}


