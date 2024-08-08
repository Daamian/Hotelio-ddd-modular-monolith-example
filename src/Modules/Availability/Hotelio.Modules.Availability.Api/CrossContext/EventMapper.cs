using Hotelio.CrossContext.Contract.Shared.Message;
using Hotelio.Modules.Availability.Domain.Event;
using Hotelio.Shared.Event;
using ResourceBookedContract = Hotelio.CrossContext.Contract.Availability.Event.ResourceBooked;
using MediatR;

namespace Hotelio.Modules.Availability.Api.CrossContext;

internal class EventMapper: INotificationHandler<ResourceBooked>
{
    private readonly IMessageDispatcher _messageDispatcher;

    public EventMapper(IMessageDispatcher messageDispatcher)
    {
        _messageDispatcher = messageDispatcher;
    }
    
    public async Task Handle(ResourceBooked domainEvent, CancellationToken cancelationToken)
    {
        await _messageDispatcher.DispatchAsync(
            new ResourceBookedContract(
                domainEvent.ExternalId, 
                domainEvent.OwnerId,
                domainEvent.StartDate,
                domainEvent.EndDate
                )
            );
    }
}