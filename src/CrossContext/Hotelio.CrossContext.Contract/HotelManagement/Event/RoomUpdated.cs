namespace Hotelio.CrossContext.Contract.HotelManagement.Event;

public record RoomUpdated(string HotelId, string RoomId, int MaxGuests, string Type);