using Hotelio.Modules.HotelManagement.Core.Service.DTO;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal interface IRoomTypeService
{
    public Task<int> AddAsync(RoomTypeDto roomTypeDto);
    public Task UpdateAsync(RoomTypeDto roomTypeDto);
    public Task<RoomTypeDto?> GetAsync(int id);
}