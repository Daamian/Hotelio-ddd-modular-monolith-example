using Hotelio.CrossContext.Contract.Availability;
using Hotelio.CrossContext.Contract.Availability.Event;
using Hotelio.CrossContext.Contract.Booking;
using Hotelio.CrossContext.Contract.Booking.Event;
using Hotelio.CrossContext.Contract.Catalog;
using Hotelio.CrossContext.Contract.Shared.Exception;
using MediatR;

namespace Hotelio.CrossContext.Saga.ReservationProcess;

internal class ReservationProcessManager: 
    INotificationHandler<ReservationCreated>, 
    INotificationHandler<ReservationCanceled>,
    INotificationHandler<ResourceBooked>,
    INotificationHandler<ResourceTypeBookRejected>,
    INotificationHandler<ReservationPayed>
{
    private readonly IAvailability _availability;
    private readonly ICatalogSearcher _catalog;
    private readonly IBooking _booking;

    public ReservationProcessManager(
        IAvailability availability, 
        ICatalogSearcher catalogSearcher,
        IBooking booking
        ) {
        _availability = availability;
        _catalog = catalogSearcher;
        _booking = booking;
    }

    public async Task Handle(ReservationCreated domainEvent, CancellationToken cancelationToken)
    {
        var reservation = await _booking.GetAsync(domainEvent.Id);
        
        if (null == reservation) {
            return;
        }

        if (reservation.IsPostPaid)
        {
            try
            {
                var roomId = await _catalog.FindFirstRoomAvailableAsync(
                    reservation.HotelId,
                    reservation.RoomType,
                    reservation.StartDate,
                    reservation.EndDate
                );
                
                await _availability.BookAsync(roomId, reservation.Id, reservation.StartDate, reservation.EndDate);
            } catch (ContractException e) {
                await _booking.RejectReservation(reservation.Id);
            }
        }
    }

    public async Task Handle(ReservationPayed domainEvent, CancellationToken cancelationToken)
    {
        var reservation = await _booking.GetAsync(domainEvent.Id);
        
        if (null == reservation) {
            return;
        }

        if (reservation.IsInAdvance)
        {
            try
            {
                var roomId = await _catalog.FindFirstRoomAvailableAsync(
                    reservation.HotelId,
                    reservation.RoomType.ToString(),
                    reservation.StartDate,
                    reservation.EndDate
                );
                
                await _availability.BookAsync(roomId, reservation.Id, reservation.StartDate, reservation.EndDate);
            } catch (ContractException e)
            {
                await _booking.RejectReservation(reservation.Id);
            }
        }
    }

    public async Task Handle(ReservationCanceled domainEvent, CancellationToken cancelationToken)
    {
        var reservation = await _booking.GetAsync(domainEvent.Id);

        if (reservation?.RoomId != null)
            await _availability.UnBookAsync(reservation.RoomId, reservation.Id);
    }

    public async Task Handle(ResourceBooked e, CancellationToken cancelationToken)
    {
        await _booking.ConfirmReservation(e.ResourceId, e.OwnerId);
    }

    public async Task Handle(ResourceTypeBookRejected e, CancellationToken cancelationToken)
    {
        await _booking.RejectReservation(e.OwnerId);
    }
}