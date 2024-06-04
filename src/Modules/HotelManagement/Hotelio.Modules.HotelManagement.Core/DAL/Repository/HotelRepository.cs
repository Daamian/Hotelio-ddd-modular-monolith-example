using Hotelio.CrossContext.Contract.HotelManagement.Event;
using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Hotelio.Shared.Event;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.HotelManagement.Core.DAL.Repository;

internal class HotelRepository: IHotelRepository
{
    private readonly HotelDbContext _db;
    private readonly IEventBus _eventBus;

    public HotelRepository(HotelDbContext db, IEventBus eventBus)
    {
        _db = db;
        _eventBus = eventBus;
    }

    public async Task AddAsync(Hotel hotel)
    {
        await _db.Hotels.AddAsync(hotel);
        await _db.SaveChangesAsync();
        await _eventBus.Publish(new HotelCreated(hotel.Id.ToString(), hotel.Name));
    } 
    public async Task<Hotel?> FindAsync(int id) => await _db.Hotels
        .Include(h => h.Rooms)
        .FirstOrDefaultAsync(h => h.Id == id);

    public async Task UpdateAsync(Hotel hotel)
    {
        _db.Hotels.Update(hotel);
        await _db.SaveChangesAsync();
        await _eventBus.Publish(new HotelUpdated(hotel.Id.ToString(), hotel.Name));
    }
}