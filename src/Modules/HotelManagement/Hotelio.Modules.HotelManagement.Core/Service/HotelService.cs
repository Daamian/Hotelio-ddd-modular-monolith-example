using Hotelio.Modules.HotelManagement.Core.DAL;
using Hotelio.Modules.HotelManagement.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal class HotelService
{
    private readonly HotelDbContext _db;

    public HotelService(HotelDbContext db)
    {
        _db = db;
    }

    public void Add(Hotel hotel)
    {
        _db.Hotels.Add(hotel);
        _db.SaveChanges();
    } 
    public Hotel? Find(int id) => _db.Hotels
        .Include(h => h.Rooms)
        .FirstOrDefault(h => h.Id == id);

    public void Update(Hotel hotel)
    {
        _db.Hotels.Update(hotel);
        _db.SaveChanges();
    }
}