namespace Hotelio.Modules.HotelManagement.Core.Service.DTO;

internal record RoomDto(
    int Id,
    int Number,
    int Type,
    int HotelId
    );