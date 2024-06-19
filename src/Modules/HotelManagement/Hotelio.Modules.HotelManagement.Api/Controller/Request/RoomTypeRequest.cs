namespace Hotelio.Modules.HotelManagement.Api.Controller.Request;

internal record RoomTypeRequest(string Name, int MaxGuests, int Level);