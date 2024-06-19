namespace Hotelio.Modules.HotelManagement.Core.Service.DTO;

internal record HotelDetailsDto(int Id, string Name, List<int>? Amenities = null, List<RoomDetailsDto>? Rooms = null) : HotelDto(Id, Name, Amenities);