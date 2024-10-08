using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Hotelio.Modules.HotelManagement.Core.Service.Exception;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal class RoomService: IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository) => _roomRepository = roomRepository;
    
    public async Task<int> AddAsync(RoomDto roomDto)
    {
        var room = MapDto(roomDto);
        await _roomRepository.AddAsync(room);
        return room.Id;
    }

    public async Task UpdateAsync(RoomDto roomDto)
    {
        var room = await _roomRepository.FindAsync(roomDto.Id);

        if (room is null)
        {
            throw new RoomNotFoundException($"Not found room with id {roomDto.Id}");
        }
        
        await _roomRepository.UpdateAsync(MapDto(roomDto, room));
    }
    
    public async Task<RoomDto?> GetAsync(int id)
    {
        var room = await _roomRepository.FindAsync(id);

        return room is null ? null : _mapModel(room);
    }

    private static Room MapDto(RoomDto roomDto, Room? room = null)
    {
        room ??= new Room();

        room.Number = roomDto.Number;
        room.RoomTypeId = roomDto.Type;
        room.HotelId = roomDto.HotelId;

        return room;
    }

    private static RoomDto _mapModel(Room room)
    {
        return new RoomDto(room.Id, room.Number, room.RoomTypeId, room.HotelId);
    }
}