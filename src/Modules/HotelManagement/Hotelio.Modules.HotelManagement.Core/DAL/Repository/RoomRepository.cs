using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;

namespace Hotelio.Modules.HotelManagement.Core.DAL.Repository;

internal class RoomRepository: IRoomRepository
{
    private readonly HotelDbContext _db;

    public RoomRepository(HotelDbContext db) => _db = db;
    
    public void Add(Room room)
    {
        _db.Rooms.Add(room);
        _db.SaveChanges();
    }

    public Room? Find(int id) => _db.Rooms.FirstOrDefault(r => r.Id == id);

    public void Update(Room room)
    {
        _db.Rooms.Update(room);
        _db.SaveChanges();
    }
}