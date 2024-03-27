using Hotelio.Modules.HotelManagement.Core.Service.DTO;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal interface IRoomService
{
    public int Add(RoomDto roomDto);
    public void Update(RoomDto roomDto);
}