using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.HotelManagement.Core.DAL.Repository;

internal class AmenityRepository : IAmenityRepository
{
    private readonly HotelDbContext _db;

    public AmenityRepository(HotelDbContext db)
    {
        _db = db;
    }
    
    public async Task AddAsync(Amenity amenity)
    {
        await _db.Amenities.AddAsync(amenity);
        await _db.SaveChangesAsync();
    }

    public async Task<Amenity?> FindAsync(int id) => await _db.Amenities.FirstOrDefaultAsync(a => a.Id == id);

    public async Task UpdateAsync(Amenity amenity)
    {
        _db.Amenities.Update(amenity);
        await _db.SaveChangesAsync();
    }
}