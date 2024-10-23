using MediatR;
using Hotelio.Modules.Booking.Domain.Repository;
using Hotelio.Shared.Exception;

namespace Hotelio.Modules.Booking.Application.Command.Handlers;

internal sealed class RejectReservationHandler : IRequestHandler<RejectReservation>
{
    private readonly IReservationRepository _reservationRepository;

    public RejectReservationHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }
    
    public async Task Handle(RejectReservation command, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.FindAsync(command.ReservationId);

        if (null == reservation)
        {
            throw new CommandFailedException($"Not found reservation with id {command.ReservationId}");
        }
        
        reservation.Reject();
        
        await _reservationRepository.UpdateAsync(reservation);
    }

}