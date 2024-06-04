using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Hotelio.Modules.HotelManagement.Core.Service.Exception;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal class HotelService: IHotelService
{
    private readonly IHotelRepository _repository;

    public HotelService(IHotelRepository repository) => _repository = repository;
    
    public async Task<int> AddAsync(HotelDto dto)
    {
        var hotel = new Hotel() { Name = dto.Name };
        await _repository.AddAsync(hotel);
        return hotel.Id;
    }

    public async Task UpdateAsync(HotelDto dto)
    {
        var hotel = await _repository.FindAsync(dto.Id);

        if (hotel is null)
        {
            throw new HotelNotFoundException($"Not found hotel with id {dto.Id}");
        }

        hotel.Name = dto.Name;
        await _repository.UpdateAsync(hotel);
    }

    public async Task<HotelDto?> GetAsync(int id)
    {
        var hotel = await _repository.FindAsync(id);

        if (hotel is null) {
            return null;
        }

        return new HotelDto(hotel.Id, hotel.Name);
    }
}