using Hotelio.Modules.HotelManagement.Core.Model;

namespace Hotelio.Modules.HotelManagement.Core.Repository;

internal interface IAmenityRepository
{
    public Task AddAsync(Amenity amenity);
    public Task<Amenity?> FindAsync(int id);
    public Task UpdateAsync(Amenity amenity);
}