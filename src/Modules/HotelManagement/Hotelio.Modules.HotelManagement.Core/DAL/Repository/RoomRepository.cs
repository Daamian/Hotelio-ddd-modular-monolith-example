using Hotelio.CrossContext.Contract.HotelManagement.Event;
using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Hotelio.Shared.Event;

namespace Hotelio.Modules.HotelManagement.Core.DAL.Repository;

internal class RoomRepository: IRoomRepository
{
    private readonly HotelDbContext _db;
    private readonly IEventBus _eventBus;

    public RoomRepository(HotelDbContext db, IEventBus eventBus)
    {
        _db = db;
        _eventBus = eventBus;
    }

    public void Add(Room room)
    {
        _db.Rooms.Add(room);
        _db.SaveChanges();
        _eventBus.Publish(new RoomAdded(
            room.HotelId.ToString(),
            room.Id.ToString(),
            room.MaxGuests,
            room.Type.ToString()));
    }

    public Room? Find(int id) => _db.Rooms.FirstOrDefault(r => r.Id == id);

    public void Update(Room room)
    {
        _db.Rooms.Update(room);
        _db.SaveChanges();
        _eventBus.Publish(new RoomUpdated(
            room.HotelId.ToString(),
            room.Id.ToString(),
            room.MaxGuests,
            room.Type.ToString()));
    }
}