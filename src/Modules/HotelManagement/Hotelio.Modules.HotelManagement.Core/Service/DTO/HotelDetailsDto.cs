namespace Hotelio.Modules.HotelManagement.Core.Service.DTO;

internal record HotelDetailsDto(int Id, string Name, List<AmenityDetailDto>? Amenities = null, List<RoomDetailsDto>? Rooms = null);