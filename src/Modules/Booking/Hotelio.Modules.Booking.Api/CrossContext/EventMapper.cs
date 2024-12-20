using Hotelio.CrossContext.Contract.Shared.Message;
using Hotelio.Modules.Booking.Domain.Event;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;
using ReservationCreatedContext = Hotelio.CrossContext.Contract.Booking.Event.ReservationCreated;
using ReservationCanceledContext = Hotelio.CrossContext.Contract.Booking.Event.ReservationCanceled;
using ReservationPayedContext = Hotelio.CrossContext.Contract.Booking.Event.ReservationPayed;
using ReservationRejectedContext = Hotelio.CrossContext.Contract.Booking.Event.ReservationRejected;
using MediatR;

namespace Hotelio.Modules.Booking.Api.CrossContext;

internal class EventMapper : INotificationHandler<ReservationCreated>, INotificationHandler<ReservationCanceled>,
    INotificationHandler<ReservationPayed>, INotificationHandler<ReservationRejected>
{
    private readonly IMessageDispatcher _messageDispatcher;
    private readonly IReservationRepository _repository;

    public EventMapper(IMessageDispatcher messageDispatcher, IReservationRepository repository)
    {
        _messageDispatcher = messageDispatcher;
        _repository = repository;
    }

    public async Task Handle(ReservationCreated domainEvent, CancellationToken cancellationToken)
    {
        var reservation = await _repository.FindAsync(domainEvent.Id);

        if (reservation is null)
        {
            return;
        }

        await _messageDispatcher.DispatchAsync(new ReservationCreatedContext(domainEvent.Id,
            PaymentType.PostPaid == reservation.PaymentType));
    }

    public async Task Handle(ReservationCanceled domainEvent, CancellationToken cancellationToken)
    {
        await _messageDispatcher.DispatchAsync(new ReservationCanceledContext(domainEvent.ReservationId));
    }

    public async Task Handle(ReservationPayed domainEvent, CancellationToken cancellationToken)
    {
        var reservation = await _repository.FindAsync(domainEvent.Id);

        if (reservation is null)
        {
            return;
        }

        await _messageDispatcher.DispatchAsync(new ReservationPayedContext(domainEvent.Id,
            (int)PaymentType.InAdvance == reservation.PaymentType));
    }

    public async Task Handle(ReservationRejected domainEvent, CancellationToken cancellationToken)
    {
        await _messageDispatcher.DispatchAsync(new ReservationRejectedContext(domainEvent.ReservationId));
    }
}