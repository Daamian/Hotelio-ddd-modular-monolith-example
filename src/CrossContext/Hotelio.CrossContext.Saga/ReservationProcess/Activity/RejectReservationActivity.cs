using Hotelio.CrossContext.Contract.Availability.Event;
using Hotelio.CrossContext.Contract.Booking.Event;
using MassTransit;

namespace Hotelio.CrossContext.Saga.ReservationProcess.Activity;

public class RejectReservationActivity : IStateMachineActivity<ReservationState, ResourceTypeBookRejected>, IStateMachineActivity<ReservationState, ReservationCreated>
{
    private readonly ReservationProcessManager _reservationProcessManager;
    
    public RejectReservationActivity(ReservationProcessManager reservationProcessManager)
    {
        _reservationProcessManager = reservationProcessManager;
    }
    
    public void Probe(ProbeContext context)
    {
        context.CreateScope("reject-reservation");
    }

    public void Accept(StateMachineVisitor visitor)
    {
        visitor.Visit(this);
    }

    public async Task Execute(BehaviorContext<ReservationState, ResourceTypeBookRejected> context, IBehavior<ReservationState, ResourceTypeBookRejected> next)
    {
        await _reservationProcessManager.RejectReservation(context.Message.OwnerId);
        await next.Execute(context).ConfigureAwait(false);
    }

    public Task Faulted<TException>(BehaviorExceptionContext<ReservationState, ResourceTypeBookRejected, TException> context, IBehavior<ReservationState, ResourceTypeBookRejected> next) where TException : Exception
    {
        return next.Faulted(context);
    }

    public async Task Execute(BehaviorContext<ReservationState, ReservationCreated> context, IBehavior<ReservationState, ReservationCreated> next)
    {
        await _reservationProcessManager.RejectReservation(context.Message.Id);
        await next.Execute(context).ConfigureAwait(false);
    }

    public Task Faulted<TException>(BehaviorExceptionContext<ReservationState, ReservationCreated, TException> context, IBehavior<ReservationState, ReservationCreated> next) where TException : Exception
    {
        return next.Faulted(context);
    }
}