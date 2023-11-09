using Hotelio.Modules.Booking.Application.Client;
using Hotelio.Modules.Booking.Application.Client.Availability;
using Hotelio.Modules.Booking.Application.Command;
using Hotelio.Modules.Booking.Application.Event.External;
using Hotelio.Modules.Booking.Application.ReadModel;
using Hotelio.Modules.Booking.Domain.Event;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;
using Reservation = Hotelio.Modules.Booking.Domain.Model.Reservation;

namespace Hotelio.Modules.Booking.Application.Saga;

using Hotelio.Modules.Booking.Domain.Event;

/**
 * TODO:
 * 1. Implementation of available service to book and unbook resource and dispatch events (ResourceBooked | ResourceBookRejected)
 * 2. Implementation ConfirmReservation command
 * 3. Implementation RejectReservation command
 * 4. Implementation dispatch ReservationCreated event on bus
 * 5. Register Process manager as event handler
 */
internal class ReservationProcessManager
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

    public async void HandleReservationCreated(ReservationCreated domainEvent)
    {
        var reservation = await _readModel.FindAsync(new Guid(domainEvent.id));
        
        if (null == reservation) {
            return;
        }

        if (reservation.PaymentType == PaymentType.PostPaid.ToString())
        {
            var roomId = await _hotelApiClient.GetFirstAvailableRoom(
                reservation.RoomType.ToString(), 
                reservation.StartDate, 
                reservation.EndDate
                );
            
            await this._availabilityApi.Book(roomId, reservation.Id.ToString(), reservation.StartDate, reservation.EndDate);
        }
    }

    public async void HandleResourceBooked(ResourceBooked e)
    {
        //TODO consider what if confirmation is not possible
        await this._commandBus.DispatchAsync(new ConfirmReservation(e.RoomId, e.ReservationId));
    }

    public async void HandleResourceBookRejected(ResourceBookRejected e)
    {
        await this._commandBus.DispatchAsync(new RejectReservation(e.ReservationId));
    }

    public async void HandleReservationCanceled(ReservationCanceled domainEvent)
    {
        //TODO consider what if unbook is not possible
        var reservation = await _readModel.FindAsync(new Guid(domainEvent.ReservationId));

        if (null == reservation || null == reservation.RoomId) {
            return;
        }

        await this._availabilityApi.UnBook(reservation.RoomId, domainEvent.ReservationId, reservation.StartDate, reservation.EndDate);
    }
}