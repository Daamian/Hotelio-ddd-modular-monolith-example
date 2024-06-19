using Hotelio.Modules.HotelManagement.Core.Model;

namespace Hotelio.Modules.HotelManagement.Core.Repository;

internal interface IRoomTypeRepository
{
    public Task AddAsync(RoomType roomType);
    public Task<RoomType?> FindAsync(int id);
    public Task UpdateAsync(RoomType roomType);
}