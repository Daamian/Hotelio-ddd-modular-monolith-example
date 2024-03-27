using Hotelio.Modules.HotelManagement.Core.Model;

namespace Hotelio.Modules.HotelManagement.Core.Repository;

internal interface IRoomRepository
{
    public void Add(Room room);
    public Room? Find(int id);
    public void Update(Room room);
}