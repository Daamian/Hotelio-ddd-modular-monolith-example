using Hotelio.CrossContext.Contract.Availability;
using Hotelio.CrossContext.Contract.Availability.Event;
using Hotelio.Modules.Booking.Application.Command;
using Hotelio.Modules.Booking.Application.ReadModel;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;
using MediatR;

namespace Hotelio.Modules.Booking.Application.Saga;

using Hotelio.Modules.Booking.Domain.Event;

internal class ReservationProcessManager: 
    INotificationHandler<ReservationCreated>, 
    INotificationHandler<ReservationCanceled>,
    INotificationHandler<ResourceBooked>,
    INotificationHandler<ResourceTypeBookRejected>
{
    private readonly IReadModelStorage _readModel;
    private readonly IAvailability _availability;
    private readonly ICommandBus _commandBus;

    public ReservationProcessManager(IReadModelStorage readModel, IAvailability availability, ICommandBus commandBus)
    {
        _readModel = readModel;
        _availability = availability;
        _commandBus = commandBus;
    }

    public async Task Handle(ReservationCreated domainEvent, CancellationToken cancelationToken)
    {
        var reservation = await _readModel.FindAsync(new Guid(domainEvent.Id));
        
        if (null == reservation) {
            return;
        }

        if (reservation.PaymentType == PaymentType.PostPaid.ToString())
        {
            await this._availability.BookFirstAvailableAsync(
                reservation.Hotel.Id, 
                reservation.RoomType.Id,
                reservation.Id.ToString(), 
                reservation.StartDate, 
                reservation.EndDate);
        } else {
            //TODO: temp book
        }
    }
    
    public async Task Handle(ReservationCanceled domainEvent, CancellationToken cancelationToken)
    {
        //TODO consider what if unbook is not possible
        var reservation = await _readModel.FindAsync(new Guid(domainEvent.ReservationId));

        if (null == reservation || null == reservation.RoomId) {
            return;
        }

        await this._availability.UnBookAsync(reservation.RoomId, domainEvent.ReservationId);
    }

    public async Task Handle(ResourceBooked e, CancellationToken cancelationToken)
    {
        //TODO consider what if confirmation is not possible
        await this._commandBus.DispatchAsync(new ConfirmReservation(e.ResourceId, e.OwnerId));
    }

    public async Task Handle(ResourceTypeBookRejected e, CancellationToken cancelationToken)
    {
        await this._commandBus.DispatchAsync(new RejectReservation(e.OwnerId));
    }
}