using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.HotelManagement.Core.DAL.Repository;

internal class RoomTypeRepository : IRoomTypeRepository
{
    private readonly HotelDbContext _db;

    public RoomTypeRepository(HotelDbContext db)
    {
        _db = db;
    }
    
    public async Task AddAsync(RoomType roomType)
    {
        await _db.RoomTypes.AddAsync(roomType);
        await _db.SaveChangesAsync();
    }

    public async Task<RoomType?> FindAsync(int id) => await _db.RoomTypes.FirstOrDefaultAsync(r => r.Id == id);

    public async Task UpdateAsync(RoomType roomType)
    {
        _db.RoomTypes.Update(roomType);
        await _db.SaveChangesAsync();
    }
}