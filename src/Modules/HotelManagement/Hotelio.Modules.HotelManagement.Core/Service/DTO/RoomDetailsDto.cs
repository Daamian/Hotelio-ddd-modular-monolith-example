namespace Hotelio.Modules.HotelManagement.Core.Service.DTO;

internal record RoomDetailsDto(
    int Id,
    int Number,
    int Type,
    int HotelId,
    RoomTypeDto TypeDetails
    ): RoomDto(Id, Number, Type, HotelId);