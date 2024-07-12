using Hotelio.Modules.Booking.Domain.Repository;
using Hotelio.Shared.Exception;
using MediatR;

namespace Hotelio.Modules.Booking.Application.Command.Handlers;

internal sealed class PayReservationHandler: IRequestHandler<PayReservation>
{
    private readonly IReservationRepository _reservationRepository;

    public PayReservationHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }
    
    public async Task Handle(PayReservation command, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.FindAsync(command.ReservationId);
        
        if (null == reservation)
        {
            throw new CommandFailedException($"Not found reservation with id {command.ReservationId}");
        }
        
        reservation.Pay(command.Price);
        await _reservationRepository.UpdateAsync(reservation);
    }
}
