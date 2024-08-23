using Hotelio.CrossContext.Contract.Shared.Message;
using Hotelio.Modules.Booking.Domain.Event;
using ReservationCreatedContext = Hotelio.CrossContext.Contract.Booking.Event.ReservationCreated;
using ReservationCanceledContext = Hotelio.CrossContext.Contract.Booking.Event.ReservationCanceled;
using ReservationPayedContext = Hotelio.CrossContext.Contract.Booking.Event.ReservationPayed;
using MediatR;

namespace Hotelio.Modules.Booking.Api.CrossContext;

internal class EventMapper: INotificationHandler<ReservationCreated>, INotificationHandler<ReservationCanceled>, INotificationHandler<ReservationPayed>
{
    private readonly IMessageDispatcher _messageDispatcher;

    public EventMapper(IMessageDispatcher messageDispatcher)
    {
        _messageDispatcher = messageDispatcher;
    }
    
    public async Task Handle(ReservationCreated domainEvent, CancellationToken cancellationToken)
    {
        await _messageDispatcher.DispatchAsync(new ReservationCreatedContext(domainEvent.Id));
    }

    public async Task Handle(ReservationCanceled domainEvent, CancellationToken cancellationToken)
    {
        await _messageDispatcher.DispatchAsync(new ReservationCanceledContext(domainEvent.ReservationId));
    }

    public async Task Handle(ReservationPayed domainEvent, CancellationToken cancellationToken)
    {
        await _messageDispatcher.DispatchAsync(new ReservationPayedContext(domainEvent.Id));
    }
}