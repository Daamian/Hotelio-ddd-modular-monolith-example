using Hotelio.Shared.Event;

namespace Hotelio.CrossContext.Contract.HotelManagement.Event;

public record HotelUpdated(string Id, string Name): IEvent;