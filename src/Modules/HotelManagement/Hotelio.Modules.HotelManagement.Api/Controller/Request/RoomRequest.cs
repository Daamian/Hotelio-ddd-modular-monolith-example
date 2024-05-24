namespace Hotelio.Modules.HotelManagement.Api.Controller.Request;

internal record RoomRequest(int Number, int MaxGuests, int Type, int HotelId);