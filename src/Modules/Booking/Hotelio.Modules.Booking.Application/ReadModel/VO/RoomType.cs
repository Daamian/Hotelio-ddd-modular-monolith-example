using System;

namespace Hotelio.Modules.Booking.Application.ReadModel.VO;

internal class RoomType
{
    public int Id { set; get; }
    public String Name { set; get; } = "";

    public RoomType(int Id, String Name)
    {
        this.Id = Id;
        this.Name = Name;
    }
}


