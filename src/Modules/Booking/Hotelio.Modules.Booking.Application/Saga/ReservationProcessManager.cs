using Hotelio.CrossContext.Contract.Availability;
using Hotelio.CrossContext.Contract.Availability.Event;
using Hotelio.CrossContext.Contract.Shared.Exception;
using Hotelio.Modules.Booking.Application.Command;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;
using Hotelio.Shared.Commands;
using MediatR;

namespace Hotelio.Modules.Booking.Application.Saga;

using Hotelio.Modules.Booking.Domain.Event;

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

    public ReservationProcessManager(IReservationRepository reservationRepository, IAvailability availability, ICommandBus commandBus)
    {
        _reservationRepository = reservationRepository;
        _availability = availability;
        _commandBus = commandBus;
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
                var roomId = "test";
                // var roomId = await._catalog.FindFirstAvailableAsync(reservation.HotelId, reservation.RoomType)
                // await._availability.BookAsync(roomId)
                await this._availability.BookAsync(
                    roomId,
                    reservation.Id.ToString(), 
                    reservation.StartDate, 
                    reservation.EndDate);
            } catch (ContractException e) {
                await this._commandBus.DispatchAsync(new RejectReservation(reservation.Id));
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
                var roomId = "test";
                // var roomId = await._catalog.FindFirstAvailableAsync(reservation.HotelId, reservation.RoomType)
                // await._availability.BookAsync(roomId)
                await this._availability.BookAsync(
                    roomId,
                    reservation.Id,
                    reservation.StartDate,
                    reservation.EndDate);
            } catch (ContractException e) {
                await this._commandBus.DispatchAsync(new RejectReservation(reservation.Id));
            }
        }
    }

    public async Task Handle(ReservationCanceled domainEvent, CancellationToken cancelationToken)
    {
        var reservationAg = await _reservationRepository.FindAsync(domainEvent.ReservationId);

        if (null == reservationAg || null == reservationAg.Snap().RoomId) {
            return;
        }
        
        var reservation = reservationAg.Snap();

        await this._availability.UnBookAsync(reservation.RoomId, domainEvent.ReservationId);
    }

    public async Task Handle(ResourceBooked e, CancellationToken cancelationToken)
    {
        await this._commandBus.DispatchAsync(new ConfirmReservation(e.ResourceId, e.OwnerId));
    }

    public async Task Handle(ResourceTypeBookRejected e, CancellationToken cancelationToken)
    {
        await this._commandBus.DispatchAsync(new RejectReservation(e.OwnerId));
    }
}