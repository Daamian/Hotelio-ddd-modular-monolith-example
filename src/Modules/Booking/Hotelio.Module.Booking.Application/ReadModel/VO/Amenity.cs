using System;

namespace Hotelio.Module.Booking.Application.ReadModel.VO;

internal class Amenity
{
    public Guid Id { set; get; }
    public String Name { set; get; }

    public Amenity(Guid Id, String Name)
    {
        this.Id = Id;
        this.Name = Name;
    }
}


