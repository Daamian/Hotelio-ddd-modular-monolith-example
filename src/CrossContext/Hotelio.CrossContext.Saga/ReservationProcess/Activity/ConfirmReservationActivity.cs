using Hotelio.CrossContext.Contract.Availability.Event;
using MassTransit;

namespace Hotelio.CrossContext.Saga.ReservationProcess.Activity;

public class ConfirmReservationActivity : IStateMachineActivity<ReservationState, ResourceBooked>
{
    private readonly ReservationProcessManager _reservationProcessManager;
    
    public ConfirmReservationActivity(ReservationProcessManager reservationProcessManager)
    {
        _reservationProcessManager = reservationProcessManager;
    }
    
    public void Probe(ProbeContext context)
    {
        context.CreateScope("confirm-reservation");
    }

    public void Accept(StateMachineVisitor visitor)
    {
        visitor.Visit(this);
    }

    public async Task Execute(BehaviorContext<ReservationState, ResourceBooked> context, IBehavior<ReservationState, ResourceBooked> next)
    {
        await _reservationProcessManager.ConfirmReservation(context.Message.ResourceId, context.Message.OwnerId);
        await next.Execute(context).ConfigureAwait(false);
    }

    public Task Faulted<TException>(BehaviorExceptionContext<ReservationState, ResourceBooked, TException> context, IBehavior<ReservationState, ResourceBooked> next) where TException : Exception
    {
        return next.Faulted(context);
    }
}