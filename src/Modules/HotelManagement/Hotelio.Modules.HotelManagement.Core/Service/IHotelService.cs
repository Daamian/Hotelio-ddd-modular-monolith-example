using Hotelio.Modules.HotelManagement.Core.Service.DTO;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal interface IHotelService
{
    public Task<int> AddAsync(HotelDto dto);
    public Task UpdateAsync(HotelDto dto);
    public Task<HotelDto?> GetAsync(int id);
}