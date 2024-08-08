using Hotelio.CrossContext.Contract.HotelManagement.Event;
using Hotelio.CrossContext.Contract.Shared.Message;
using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Hotelio.Shared.Event;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.HotelManagement.Core.DAL.Repository;

internal class RoomRepository: IRoomRepository
{
    private readonly HotelDbContext _db;
    private readonly IMessageDispatcher _messageDispatcher;

    public RoomRepository(HotelDbContext db, IMessageDispatcher messageDispatcher)
    {
        _db = db;
        _messageDispatcher = messageDispatcher;
    }

    public async Task AddAsync(Room room)
    {
        await _db.Rooms.AddAsync(room);
        await _db.SaveChangesAsync();
        
        var roomAdded = await FindAsync(room.Id);
        if (roomAdded is not null)  {
            await _messageDispatcher.DispatchAsync(new RoomAdded(
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
        await _messageDispatcher.DispatchAsync(new RoomUpdated(
            room.HotelId.ToString(),
            room.Id.ToString(),
            room.Type.MaxGuests,
            room.Type.Id.ToString()));
    }
}