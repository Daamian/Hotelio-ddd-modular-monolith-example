using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Hotelio.Modules.HotelManagement.Core.Service.Exception;

namespace Hotelio.Modules.HotelManagement.Core.Service;

internal class RoomService: IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository) => _roomRepository = roomRepository;
    
    public int Add(RoomDto roomDto)
    {
        var room = MapDto(roomDto);
        _roomRepository.Add(room);
        return room.Id;
    }

    public void Update(RoomDto roomDto)
    {
        var room = _roomRepository.Find(roomDto.Id);

        if (room is null)
        {
            throw new RoomNotFoundException($"Not found room with id {roomDto.Id}");
        }
        
        _roomRepository.Update(MapDto(roomDto, room));
    }
    
    public RoomDto? Get(int id)
    {
        var room = _roomRepository.Find(id);

        if (room is null) {
            return null;
        }

        return _mapModel(room);
    }

    private static Room MapDto(RoomDto roomDto, Room? room = null)
    {
        if (room is null)
        {
            room = new Room();
        }

        room.Number = roomDto.Number;
        room.MaxGuests = roomDto.MaxGuests;
        room.Type = (RoomType)roomDto.Type;
        room.HotelId = roomDto.HotelId;

        return room;
    }

    private static RoomDto _mapModel(Room room)
    {
        return new RoomDto(room.Id, room.Number, room.MaxGuests, (int) room.Type, room.HotelId);
    }
}