using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Hotelio.Modules.HotelManagement.Core.Service.Exception;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal class AmenityService: IAmenityService
{
    private readonly IAmenityRepository _repository;

    public AmenityService(IAmenityRepository repository) => _repository = repository;
    
    public async Task<int> AddAsync(AmenityDto amenityDto)
    {
        var amenity = MapDto(amenityDto);
        await _repository.AddAsync(amenity);

        return amenity.Id;
    }

    public async Task UpdateAsync(AmenityDto amenityDto)
    {
        var amenity = await _repository.FindAsync(amenityDto.Id);

        if (amenity is null)
        {
            throw new AmenityNotFoundException($"Not found room with id {amenityDto.Id}");
        }
        
        await _repository.UpdateAsync(MapDto(amenityDto, amenity));
    }

    public async Task<AmenityDto?> GetAsync(int id)
    {
        var amenity = await _repository.FindAsync(id);

        if (amenity is null) {
            return null;
        }

        return _mapModel(amenity);
    }

    private Amenity MapDto(AmenityDto dto, Amenity? amenity = null)
    {
        if (amenity is null)
        {
            amenity = new Amenity();
        }

        amenity.Name = dto.Name;
        
        return amenity;
    }
    
    private static AmenityDto _mapModel(Amenity amenity)
    {
        return new AmenityDto(amenity.Id, amenity.Name);
    }
}