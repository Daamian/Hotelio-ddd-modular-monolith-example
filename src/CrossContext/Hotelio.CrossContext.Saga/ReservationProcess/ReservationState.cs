using MassTransit;

namespace Hotelio.CrossContext.Saga.ReservationProcess;

public class ReservationState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public Guid? RoomId { get; set; }
}