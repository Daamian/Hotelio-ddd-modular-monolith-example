using Hotelio.Modules.HotelManagement.Core.Model;

namespace Hotelio.Modules.HotelManagement.Core.Repository;

internal interface IHotelRepository
{
    public Task AddAsync(Hotel hotel);
    public Task<Hotel?> FindAsync(int id);
    public Task UpdateAsync(Hotel hotel);
}