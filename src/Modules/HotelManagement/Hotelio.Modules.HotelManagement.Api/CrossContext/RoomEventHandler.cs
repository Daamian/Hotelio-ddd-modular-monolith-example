using Hotelio.CrossContext.Contract.Availability;
using Hotelio.CrossContext.Contract.HotelManagement.Event;
using MassTransit;
using MediatR;

namespace Hotelio.Modules.HotelManagement.Api.CrossContext;

public class RoomEventHandler: IConsumer<RoomAdded>
{
    private readonly IAvailability _availability;

    public RoomEventHandler(IAvailability availability)
    {
        _availability = availability;
    }

    public async Task Consume(ConsumeContext<RoomAdded> context)
    {
        var contractEvent = context.Message;
        await _availability.CreateResource(contractEvent.RoomId);
    }
}