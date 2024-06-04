using Hotelio.Modules.Catalog.Core.Model;

namespace Hotelio.Modules.Catalog.Core.Repository;

internal interface IHotelRepository
{
    public Task AddAsync(Hotel hotel);
    public Task UpdateAsync(Hotel hotel);
    public Task<Hotel?> FindAsync(string id);
    public Task<Hotel> FindByRoomAsync(string roomId);
    public Task<Room?> FindFirstRoomAvailableAsync(string hotelId, string type, DateTime startDate, DateTime endDate);
}