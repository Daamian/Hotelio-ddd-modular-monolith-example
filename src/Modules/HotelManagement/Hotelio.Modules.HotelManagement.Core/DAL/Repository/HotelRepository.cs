using Hotelio.CrossContext.Contract.HotelManagement.Event;
using Hotelio.CrossContext.Contract.Shared.Message;
using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Hotelio.Shared.Event;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.HotelManagement.Core.DAL.Repository;

internal class HotelRepository: IHotelRepository
{
    private readonly HotelDbContext _db;
    private readonly IMessageDispatcher _messageDispatcher;

    public HotelRepository(HotelDbContext db, IMessageDispatcher messageDispatcher)
    {
        _db = db;
        _messageDispatcher = messageDispatcher;
    }

    public async Task AddAsync(Hotel hotel)
    {
        await _db.Hotels.AddAsync(hotel);
        await _db.SaveChangesAsync();
        await _messageDispatcher.DispatchAsync(new HotelCreated(hotel.Id.ToString(), hotel.Name));
    } 
    public async Task<Hotel?> FindAsync(int id) => await _db.Hotels
        .Include(h => h.Rooms).ThenInclude(r => r.Type)
        .Include(h => h.Amenities)
        .FirstOrDefaultAsync(h => h.Id == id);

    public async Task UpdateAsync(Hotel hotel)
    {
        _db.Hotels.Update(hotel);
        await _db.SaveChangesAsync();
        await _messageDispatcher.DispatchAsync(new HotelUpdated(hotel.Id.ToString(), hotel.Name));
    }
}