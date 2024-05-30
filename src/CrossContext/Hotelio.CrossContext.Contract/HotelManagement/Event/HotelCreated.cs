using Hotelio.Shared.Event;

namespace Hotelio.CrossContext.Contract.HotelManagement.Event;

public record HotelCreated(string Id, string Name): IEvent;