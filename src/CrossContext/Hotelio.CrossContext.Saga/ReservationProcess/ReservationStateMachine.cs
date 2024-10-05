using Hotelio.CrossContext.Contract.Availability.Event;
using Hotelio.CrossContext.Contract.Booking.Event;
using Hotelio.CrossContext.Saga.ReservationProcess.Activity;
using MassTransit;

namespace Hotelio.CrossContext.Saga.ReservationProcess;

public class ReservationStateMachine : MassTransitStateMachine<ReservationState>
{
    public State AwaitingPayment { get; private set; }
    public State AwaitingConfirmation { get; private set; }
    public State Completed { get; private set; }
    public State Canceled { get; private set; }

    public Event<ReservationCreated> ReservationCreated { get; private set; }
    public Event<ReservationPayed> ReservationPayed { get; private set; }
    public Event<ResourceBooked> ResourceBooked { get; private set; }
    public Event<ResourceTypeBookRejected> ResourceTypeBookRejected { get; private set; }
    public Event<ReservationCanceled> ReservationCanceled { get; private set; }

    public ReservationStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => ReservationCreated, x => x.CorrelateById(context => new Guid(context.Message.Id)));
        Event(() => ReservationPayed, x => x.CorrelateById(context => new Guid(context.Message.Id)));
        Event(() => ResourceBooked, x => x.CorrelateById(context => new Guid(context.Message.OwnerId)));
        Event(() => ResourceTypeBookRejected, x => x.CorrelateById(context => new Guid(context.Message.OwnerId)));
        Event(() => ReservationCanceled, x => x.CorrelateById(context => new Guid(context.Message.Id)));
        
        Initially(
            When(ReservationCreated)
                .IfElse(context => context.Message.IsPostPaid, 
                    x => 
                        x.Activity(selector => selector.OfType<BookResourceOnReservationCreatedActivity>())
                        .TransitionTo(AwaitingConfirmation),
                    x => x.TransitionTo(AwaitingPayment)
                ));


        During(AwaitingPayment,
            When(ReservationPayed)
                .If(context => context.Message.IsPayInAdvance,
                    x => x.Activity(selector => selector.OfType<BookResourceOnReservationPayedActivity>())
                        .TransitionTo(AwaitingConfirmation)
                    ));

        During(AwaitingConfirmation,
            When(ResourceBooked)
                .Activity(a => a.OfType<ConfirmReservationActivity>())
                .TransitionTo(Completed)
        );
        
        During(AwaitingConfirmation,
            When(ResourceTypeBookRejected)
                .Activity(a => a.OfType<RejectReservationActivity>())
                .TransitionTo(Canceled)
        );

        DuringAny(
            When(ReservationCanceled)
                .Activity(a => a.OfType<UnBookResourceOnReservationCanceledActivity>())
                .TransitionTo(Canceled)
        );
    }

    private void MarkAsComplete(ReservationState instance)
    {
        instance.RoomId = null;
    }
}