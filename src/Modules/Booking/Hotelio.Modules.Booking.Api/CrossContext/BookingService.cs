using Hotelio.CrossContext.Contract.Booking;
using Hotelio.Modules.Booking.Application.Command;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;
using Hotelio.Shared.Commands;
using Reservation = Hotelio.CrossContext.Contract.Booking.DTO.Reservation;

namespace Hotelio.Modules.Booking.Api.CrossContext;

internal class BookingService : IBooking
{
    private readonly ICommandBus _commandBus;
    private readonly IReservationRepository _repository;

    public BookingService(ICommandBus commandBus, IReservationRepository repository)
    {
        _commandBus = commandBus;
        _repository = repository;
    }

    public async Task<Reservation?> GetAsync(string id)
    {
        var reservation = await _repository.FindAsync(id);

        if (reservation is null)
        {
            return null;
        }
        
        var paymentType = reservation.PaymentType;

        return new Reservation(
            reservation.Id,
            reservation.HotelId,
            reservation.RoomType.ToString(),
            reservation.DateRange.StartDate,
            reservation.DateRange.EndDate,
            paymentType == PaymentType.PostPaid,
            paymentType == PaymentType.InAdvance,
            reservation.RoomId);
    }

    public async Task RejectReservation(string id)
    {
        await _commandBus.DispatchAsync(new RejectReservation(id));
    }

    public async Task ConfirmReservation(string roomId, string reservationId)
    {
        await _commandBus.DispatchAsync(new ConfirmReservation(roomId, reservationId));
    }
}