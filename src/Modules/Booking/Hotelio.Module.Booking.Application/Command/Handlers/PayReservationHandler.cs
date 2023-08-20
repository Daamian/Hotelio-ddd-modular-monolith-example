using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Domain.Model;
using Hotelio.Shared.Commands;

internal sealed class PayReservationHandler : ICommandHandler<PayReservation>
{
    public async Task HandleAsync(PayReservation command)
    {
        throw new NotImplementedException();
    }
}
