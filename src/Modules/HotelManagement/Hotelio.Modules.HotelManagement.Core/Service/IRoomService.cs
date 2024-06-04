using Hotelio.Modules.HotelManagement.Core.Service.DTO;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal interface IRoomService
{
    public Task<int> AddAsync(RoomDto roomDto);
    public Task UpdateAsync(RoomDto roomDto);
    public Task<RoomDto?> GetAsync(int id);
}