using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Hotelio.Modules.HotelManagement.Core.Service.Exception;
using Microsoft.IdentityModel.Tokens;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal class HotelService: IHotelService
{
    private readonly IHotelRepository _repository;
    private readonly IAmenityRepository _amenityRepository;

    public HotelService(IHotelRepository repository, IAmenityRepository amenityRepository)
    {
        _repository = repository;
        _amenityRepository = amenityRepository;
    } 
    
    public async Task<int> AddAsync(HotelDto dto)
    {
        var hotel = new Hotel() { Name = dto.Name, Amenities = await _mapAmenities(dto.Amenities)};
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
        hotel.Amenities = await _mapAmenities(dto.Amenities);
        await _repository.UpdateAsync(hotel);
    }

    public async Task<HotelDetailsDto?> GetAsync(int id)
    {
        var hotel = await _repository.FindAsync(id);

        if (hotel is null) {
            return null;
        }

        return new HotelDetailsDto(hotel.Id, hotel.Name, _mapAmenitiesDto(hotel.Amenities), _mapRooms(hotel.Rooms));
    }

    private async Task<List<Amenity>> _mapAmenities(List<int>? amenitiesIds)
    {
        var amenities = new List<Amenity>();
        
        if (amenitiesIds is null)
        {
            return amenities;
        }
        
        foreach (var amenityId in amenitiesIds)
        {
            var amenity = await _amenityRepository.FindAsync(amenityId);

            if (amenity is null)
            {
                throw new AmenityNotFoundException($"Amenity with id {amenityId} not found");
            }
            
            amenities.Add(amenity);
        }

        return amenities;
    }
    
    private List<AmenityDetailDto>? _mapAmenitiesDto(List<Amenity> amenities)
    {
        return amenities.IsNullOrEmpty() ? null : amenities.Select(amenity => new AmenityDetailDto(amenity.Id, amenity.Name)).ToList();
    }
    
    private List<RoomDetailsDto>? _mapRooms(List<Room> rooms)
    {
        return rooms.IsNullOrEmpty() ? null : rooms.Select(room => new RoomDetailsDto(
            room.Id, 
            room.Number, 
            room.RoomTypeId, 
            room.HotelId,
            new RoomTypeDto(room.Type.Id, room.Type.Name, room.Type.MaxGuests, room.Type.Level))
        ).ToList();
    }
}