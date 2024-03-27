namespace Hotelio.Modules.HotelManagement.Core.Service.DTO;

internal record RoomDto(
    int Id,
    int Number,
    int MaxGuests,
    int Type,
    int HotelId
    );