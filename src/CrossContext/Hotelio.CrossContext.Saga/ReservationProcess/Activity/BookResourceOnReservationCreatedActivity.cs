using Hotelio.CrossContext.Contract.Booking.Event;
using MassTransit;

namespace Hotelio.CrossContext.Saga.ReservationProcess.Activity;

internal class BookResourceOnReservationCreatedActivity : IStateMachineActivity<ReservationState, ReservationCreated>
{
    private readonly ReservationProcessManager _reservationProcessManager;
    
    public BookResourceOnReservationCreatedActivity(ReservationProcessManager reservationProcessManager)
    {
        _reservationProcessManager = reservationProcessManager;
    }
    
    public void Probe(ProbeContext context)
    {
        context.CreateScope("book-resource");
    }

    public void Accept(StateMachineVisitor visitor)
    {
        visitor.Visit(this);
    }

    public async Task Execute(BehaviorContext<ReservationState, ReservationCreated> context, IBehavior<ReservationState, ReservationCreated> next)
    {
        await _reservationProcessManager.TryToBookResource(context.Message.Id);
        await next.Execute(context).ConfigureAwait(false);
    }

    public Task Faulted<TException>(BehaviorExceptionContext<ReservationState, ReservationCreated, TException> context, IBehavior<ReservationState, ReservationCreated> next) where TException : Exception
    {
        return next.Faulted(context);
    }
}