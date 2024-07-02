using Hotelio.CrossContext.Contract.HotelManagement.Event;
using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Hotelio.Shared.Event;
using Microsoft.EntityFrameworkCore;

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

    public async Task AddAsync(Room room)
    {
        await _db.Rooms.AddAsync(room);
        await _db.SaveChangesAsync();
        
        var roomAdded = await FindAsync(room.Id);
        if (roomAdded is not null)  {
            await _eventBus.Publish(new RoomAdded(
                roomAdded.HotelId.ToString(),
                roomAdded.Id.ToString(),
                roomAdded.Type.MaxGuests,
                roomAdded.Type.Id.ToString()));
        }
    }

    public async Task<Room?> FindAsync(int id) => await _db.Rooms
        .Include( r => r.Type)
        .FirstOrDefaultAsync(r => r.Id == id);

    public async Task UpdateAsync(Room room)
    {
        _db.Rooms.Update(room);
        await _db.SaveChangesAsync();
        await _eventBus.Publish(new RoomUpdated(
            room.HotelId.ToString(),
            room.Id.ToString(),
            room.Type.MaxGuests,
            room.Type.Id.ToString()));
    }
}