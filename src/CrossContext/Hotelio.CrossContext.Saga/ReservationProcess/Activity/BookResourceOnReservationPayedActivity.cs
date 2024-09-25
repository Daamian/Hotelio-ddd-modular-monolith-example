using Hotelio.CrossContext.Contract.Booking.Event;
using MassTransit;

namespace Hotelio.CrossContext.Saga.ReservationProcess.Activity;

public class BookResourceOnReservationPayedActivity : IStateMachineActivity<ReservationState, ReservationPayed>
{
    private readonly ReservationProcessManager _reservationProcessManager;
    
    public BookResourceOnReservationPayedActivity(ReservationProcessManager reservationProcessManager)
    {
        _reservationProcessManager = reservationProcessManager;
    }
    
    public void Probe(ProbeContext context)
    {
        context.CreateScope("book-resource-reservation-payed");
    }

    public void Accept(StateMachineVisitor visitor)
    {
        visitor.Visit(this);
    }

    public async Task Execute(BehaviorContext<ReservationState, ReservationPayed> context, IBehavior<ReservationState, ReservationPayed> next)
    {
        await _reservationProcessManager.TryToBookResource(context.Message.Id);
        await next.Execute(context).ConfigureAwait(false);
    }

    public Task Faulted<TException>(BehaviorExceptionContext<ReservationState, ReservationPayed, TException> context, IBehavior<ReservationState, ReservationPayed> next) where TException : Exception
    {
        return next.Faulted(context);
    }
}