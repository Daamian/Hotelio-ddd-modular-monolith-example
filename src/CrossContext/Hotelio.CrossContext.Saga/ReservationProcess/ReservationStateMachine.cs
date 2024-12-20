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
    public State Rejected { get; private set; }

    public Event<ReservationCreated> ReservationCreated { get; private set; }
    public Event<ReservationPayed> ReservationPayed { get; private set; }
    public Event<ResourceBooked> ResourceBooked { get; private set; }
    public Event<ResourceBookRejected> ResourceBookRejected { get; private set; }
    public Event<ReservationCanceled> ReservationCanceled { get; private set; }
    public Event<ReservationRejected> ReservationRejected { get; private set; }

    public ReservationStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => ReservationCreated, x => x.CorrelateById(context => new Guid(context.Message.Id)));
        Event(() => ReservationPayed, x => x.CorrelateById(context => new Guid(context.Message.Id)));
        Event(() => ResourceBooked, x => x.CorrelateById(context => new Guid(context.Message.OwnerId)));
        Event(() => ResourceBookRejected, x => x.CorrelateById(context => new Guid(context.Message.OwnerId)));
        Event(() => ReservationCanceled, x => x.CorrelateById(context => new Guid(context.Message.Id)));
        Event(() => ReservationRejected, x => x.CorrelateById(context => new Guid(context.Message.Id)));
        
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
        
        //TODO: retry book room
        During(AwaitingConfirmation,
            When(ResourceBookRejected)
                .Activity(a => a.OfType<RejectReservationActivity>())
        );

        DuringAny(
            When(ReservationCanceled)
                .Activity(a => a.OfType<UnBookResourceOnReservationCanceledActivity>())
                .TransitionTo(Canceled)
        );
        
        DuringAny(
            When(ReservationRejected)
                .TransitionTo(Rejected)
        );
    }

    private void MarkAsComplete(ReservationState instance)
    {
        instance.RoomId = null;
    }
}