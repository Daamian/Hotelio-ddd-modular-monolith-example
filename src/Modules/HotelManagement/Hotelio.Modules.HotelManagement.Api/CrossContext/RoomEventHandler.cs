using Hotelio.CrossContext.Contract.Availability;
using Hotelio.CrossContext.Contract.HotelManagement.Event;
using MediatR;

namespace Hotelio.Modules.HotelManagement.Api.CrossContext;

public class RoomEventHandler: INotificationHandler<RoomAdded>
{
    private readonly IAvailability _availability;

    public RoomEventHandler(IAvailability availability)
    {
        _availability = availability;
    }
    
    public async Task Handle(RoomAdded contractEvent, CancellationToken cancellationToken)
    {
        await _availability.CreateResource(contractEvent.RoomId);
    }
}