using Hotelio.Modules.HotelManagement.Core.Service.DTO;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal interface IAmenityService
{
    public Task<int> AddAsync(AmenityDto amenityDto);
    public Task UpdateAsync(AmenityDto amenityDto);
    public Task<AmenityDto?> GetAsync(int id);
}