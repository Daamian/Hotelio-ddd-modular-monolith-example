using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;
using Hotelio.Shared.Commands;
using Hotelio.Shared.Exception;
using MediatR;

namespace Hotelio.Modules.Booking.Application.Command.Handlers;


internal sealed class ConfirmReservationHandler: IRequestHandler<ConfirmReservation>
{
    private readonly IReservationRepository _reservationRepository;

    public ConfirmReservationHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task Handle(ConfirmReservation command, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.FindAsync(command.ReservationId);

        if (null == reservation)
        {
            throw new CommandFailedException($"Not found reservation with id {command.ReservationId}");
        }
        
        reservation.Confirm(command.RoomId);
        await _reservationRepository.UpdateAsync(reservation);
    }
}


