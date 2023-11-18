using Hotelio.Modules.Booking.Application.Client;
using Hotelio.Modules.Booking.Application.Client.Availability;
using Hotelio.Modules.Booking.Application.Command;
using Hotelio.Modules.Booking.Application.Event.External;
using Hotelio.Modules.Booking.Application.ReadModel;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;
using MediatR;

namespace Hotelio.Modules.Booking.Application.Saga;

using Hotelio.Modules.Booking.Domain.Event;

/**
 * TODO:
 * 1. Implementation of available service to book and unbook resource and dispatch events (RoomBooked | RoomTypeBookRejected) -> done
 * 2. Implementation ConfirmReservation command -> done
 * 3. Implementation RejectReservation command -> done
 * 4. Implementation dispatch ReservationCreated event on bus -> done
 * 5. Register Process manager as event handler -> done
 * 6. Fix null warnings
 */
internal class ReservationProcessManager: 
    INotificationHandler<ReservationCreated>, 
    INotificationHandler<ReservationCanceled>,
    INotificationHandler<RoomBooked>,
    INotificationHandler<RoomTypeBookRejected>
{
    private readonly IReadModelStorage _readModel;
    private readonly IAvailabilityApiClient _availabilityApi;
    private readonly ICommandBus _commandBus;
    private readonly IHotelApiClient _hotelApiClient;

    public ReservationProcessManager(IReadModelStorage readModel, IAvailabilityApiClient availabilityApi, ICommandBus commandBus, IHotelApiClient hotelApiClient)
    {
        _readModel = readModel;
        _availabilityApi = availabilityApi;
        _commandBus = commandBus;
        _hotelApiClient = hotelApiClient;
    }

    public async Task Handle(ReservationCreated domainEvent, CancellationToken cancelationToken)
    {
        var reservation = await _readModel.FindAsync(new Guid(domainEvent.Id));
        
        if (null == reservation) {
            return;
        }

        if (reservation.PaymentType == PaymentType.PostPaid.ToString())
        {
            await this._availabilityApi.Book(
                reservation.Hotel.Id, 
                reservation.RoomType.ToString(),
                reservation.Id.ToString(), 
                reservation.StartDate, 
                reservation.EndDate);
        }
    }
    
    public async Task Handle(ReservationCanceled domainEvent, CancellationToken cancelationToken)
    {
        //TODO consider what if unbook is not possible
        var reservation = await _readModel.FindAsync(new Guid(domainEvent.ReservationId));

        if (null == reservation || null == reservation.RoomId) {
            return;
        }

        await this._availabilityApi.UnBook(reservation.RoomId, domainEvent.ReservationId, reservation.StartDate, reservation.EndDate);
    }

    public async Task Handle(RoomBooked e, CancellationToken cancelationToken)
    {
        //TODO consider what if confirmation is not possible
        await this._commandBus.DispatchAsync(new ConfirmReservation(e.RoomId, e.ReservationId));
    }

    public async Task Handle(RoomTypeBookRejected e, CancellationToken cancelationToken)
    {
        await this._commandBus.DispatchAsync(new RejectReservation(e.ReservationId));
    }
}