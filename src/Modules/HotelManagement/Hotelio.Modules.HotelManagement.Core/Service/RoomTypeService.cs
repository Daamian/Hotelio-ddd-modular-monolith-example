using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Hotelio.Modules.HotelManagement.Core.Service.Exception;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal class RoomTypeService: IRoomTypeService
{
    private readonly IRoomTypeRepository _repository;

    public RoomTypeService(IRoomTypeRepository repository) => _repository = repository;
    
    public async Task<int> AddAsync(RoomTypeDto roomTypeDto)
    {
        var roomType = MapDto(roomTypeDto);
        await _repository.AddAsync(roomType);

        return roomType.Id;
    }

    public async Task UpdateAsync(RoomTypeDto roomTypeDto)
    {
        var room = await _repository.FindAsync(roomTypeDto.Id);

        if (room is null)
        {
            throw new RoomTypeNotFoundException($"Not found room with id {roomTypeDto.Id}");
        }
        
        await _repository.UpdateAsync(MapDto(roomTypeDto, room));
    }

    public async Task<RoomTypeDto?> GetAsync(int id)
    {
        var roomType = await _repository.FindAsync(id);

        if (roomType is null) {
            return null;
        }

        return _mapModel(roomType);
    }

    private RoomType MapDto(RoomTypeDto dto, RoomType? roomType = null)
    {
        if (roomType is null)
        {
            roomType = new RoomType();
        }

        roomType.Name = dto.Name;
        roomType.MaxGuests = dto.MaxGuests;
        roomType.Level = dto.Level;
        
        return roomType;
    }
    
    private static RoomTypeDto _mapModel(RoomType roomType)
    {
        return new RoomTypeDto(roomType.Id, roomType.Name, roomType.MaxGuests, roomType.Level);
    }
}