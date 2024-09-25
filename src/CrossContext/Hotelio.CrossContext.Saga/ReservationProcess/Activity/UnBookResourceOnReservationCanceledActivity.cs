using Hotelio.CrossContext.Contract.Booking.Event;
using MassTransit;

namespace Hotelio.CrossContext.Saga.ReservationProcess.Activity;

public class UnBookResourceOnReservationCanceledActivity : IStateMachineActivity<ReservationState, ReservationCanceled>
{
    private readonly ReservationProcessManager _reservationProcessManager;
    
    public UnBookResourceOnReservationCanceledActivity(ReservationProcessManager reservationProcessManager)
    {
        _reservationProcessManager = reservationProcessManager;
    }
    
    public void Probe(ProbeContext context)
    {
        context.CreateScope("unbook-resource-on-cancel-reservation");
    }

    public void Accept(StateMachineVisitor visitor)
    {
        visitor.Visit(this);
    }

    public async Task Execute(BehaviorContext<ReservationState, ReservationCanceled> context, IBehavior<ReservationState, ReservationCanceled> next)
    {
        await _reservationProcessManager.UnBookResource(context.Message.Id);
        await next.Execute(context).ConfigureAwait(false);
    }

    public Task Faulted<TException>(BehaviorExceptionContext<ReservationState, ReservationCanceled, TException> context, IBehavior<ReservationState, ReservationCanceled> next) where TException : Exception
    {
        return next.Faulted(context);
    }
}