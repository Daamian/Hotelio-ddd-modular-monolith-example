using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Hotelio.Modules.HotelManagement.Core.Service.Exception;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal class HotelService: IHotelService
{
    private readonly IHotelRepository _repository;

    public HotelService(IHotelRepository repository) => _repository = repository;
    
    public int Add(HotelDto dto)
    {
        var hotel = new Hotel() { Name = dto.Name };
        _repository.Add(hotel);
        return hotel.Id;
    }

    public void Update(HotelDto dto)
    {
        var hotel = _repository.Find(dto.Id);

        if (hotel is null)
        {
            throw new HotelNotFoundException($"Not found hotel with id {dto.Id}");
        }

        hotel.Name = dto.Name;
        _repository.Update(hotel);
    }

    public HotelDto? Get(int id)
    {
        var hotel = _repository.Find(id);

        if (hotel is null) {
            return null;
        }

        return new HotelDto(hotel.Id, hotel.Name);
    }
}