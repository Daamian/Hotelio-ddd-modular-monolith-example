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

    public void Add(Hotel hotel)
    {
        _db.Hotels.Add(hotel);
        _db.SaveChanges();
        _eventBus.Publish(new HotelCreated(hotel.Id.ToString(), hotel.Name));
    } 
    public Hotel? Find(int id) => _db.Hotels
        .Include(h => h.Rooms)
        .FirstOrDefault(h => h.Id == id);

    public void Update(Hotel hotel)
    {
        _db.Hotels.Update(hotel);
        _db.SaveChanges();
        _eventBus.Publish(new HotelUpdated(hotel.Id.ToString(), hotel.Name));
    }
}