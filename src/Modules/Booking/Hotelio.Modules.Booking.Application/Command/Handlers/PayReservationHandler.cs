using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Modules.Booking.Domain.Repository;
using Hotelio.Shared.Commands;
using Hotelio.Shared.Exception;
using MediatR;

namespace Hotelio.Modules.Booking.Application.Command.Handlers;

internal sealed class PayReservationHandler: IRequestHandler<PayReservation>
{
    private IReservationRepository _reservationRepository;

    public PayReservationHandler(IReservationRepository _reservationRepository)
    {
        this._reservationRepository = _reservationRepository;
    }
    
    public async Task Handle(PayReservation command, CancellationToken cancellationToken)
    {
        var reservation = this._reservationRepository.Find(command.ReservationId);
        
        if (null == reservation)
        {
            throw new CommandFailedException($"Not found reservation with id {command.ReservationId}");
        }
        
        reservation.Pay(command.Price);
        await this._reservationRepository.UpdateAsync(reservation);
    }
}
