using Hotelio.Shared.Event;

namespace Hotelio.CrossContext.Contract.HotelManagement.Event;

public record RoomAdded(string HotelId, string RoomId, int MaxGuests, string Type) : IEvent;