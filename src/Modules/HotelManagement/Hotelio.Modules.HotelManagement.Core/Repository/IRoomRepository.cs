using Hotelio.Modules.HotelManagement.Core.Model;

namespace Hotelio.Modules.HotelManagement.Core.Repository;

internal interface IRoomRepository
{
    public Task AddAsync(Room room);
    public Task<Room?> FindAsync(int id);
    public Task UpdateAsync(Room room);
}