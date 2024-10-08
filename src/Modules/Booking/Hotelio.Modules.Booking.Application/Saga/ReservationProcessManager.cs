using Hotelio.CrossContext.Contract.Availability;
using Hotelio.CrossContext.Contract.Availability.Event;
using Hotelio.CrossContext.Contract.Catalog;
using Hotelio.CrossContext.Contract.Shared.Exception;
using Hotelio.Modules.Booking.Application.Command;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;
using Hotelio.Shared.Commands;
using MediatR;

namespace Hotelio.Modules.Booking.Application.Saga;

using Domain.Event;

internal class ReservationProcessManager: 
    INotificationHandler<ReservationCreated>, 
    INotificationHandler<ReservationCanceled>,
    INotificationHandler<ResourceBooked>,
    INotificationHandler<ResourceTypeBookRejected>,
    INotificationHandler<ReservationPayed>
{
    private readonly IAvailability _availability;
    private readonly ICommandBus _commandBus;
    private readonly IReservationRepository _reservationRepository;
    private readonly ICatalogSearcher _catalog;

    public ReservationProcessManager(
        IReservationRepository reservationRepository, 
        IAvailability availability, 
        ICommandBus commandBus,
        ICatalogSearcher catalogSearcher
        ) {
        _reservationRepository = reservationRepository;
        _availability = availability;
        _commandBus = commandBus;
        _catalog = catalogSearcher;
    }

    public async Task Handle(ReservationCreated domainEvent, CancellationToken cancelationToken)
    {
        var reservationAg = await _reservationRepository.FindAsync(domainEvent.Id);
        
        if (null == reservationAg) {
            return;
        }

        var reservation = reservationAg.Snap();

        if (reservation.PaymentType == (int) PaymentType.PostPaid)
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
            } catch (ContractException e) {
                await _commandBus.DispatchAsync(new RejectReservation(reservation.Id));
            }
        }
    }

    public async Task Handle(ReservationPayed domainEvent, CancellationToken cancelationToken)
    {
        var reservationAg= await _reservationRepository.FindAsync(domainEvent.Id);
        
        if (null == reservationAg) {
            return;
        }

        var reservation = reservationAg.Snap();

        if (reservation.PaymentType == (int) PaymentType.InAdvance)
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
            } catch (ContractException e) {
                await _commandBus.DispatchAsync(new RejectReservation(reservation.Id));
            }
        }
    }

    public async Task Handle(ReservationCanceled domainEvent, CancellationToken cancelationToken)
    {
        var reservationAg = await _reservationRepository.FindAsync(domainEvent.ReservationId);

        if (reservationAg?.Snap().RoomId == null) {
            return;
        }
        
        var reservation = reservationAg.Snap();

        if (reservation.RoomId != null)
            await _availability.UnBookAsync(reservation.RoomId, domainEvent.ReservationId);
    }

    public async Task Handle(ResourceBooked e, CancellationToken cancelationToken)
    {
        await _commandBus.DispatchAsync(new ConfirmReservation(e.ResourceId, e.OwnerId));
    }

    public async Task Handle(ResourceTypeBookRejected e, CancellationToken cancelationToken)
    {
        await _commandBus.DispatchAsync(new RejectReservation(e.OwnerId));
    }
    
    //Saga { "id" : "uuid", "status" : "reservationCreated" }
    //Saga { "id" : "uuid", "status" : "resourceBooked" }
}