
using Hotelio.CrossContext.Saga.ReservationProcess;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotelio.CrossContext.Saga.DAL;

public class ReservationStateMap : SagaClassMap<ReservationState>
{
    protected override void Configure(EntityTypeBuilder<ReservationState> entity, ModelBuilder model)
    {
        entity.ToTable("ReservationStates");
        entity.Property(x => x.CurrentState).HasMaxLength(64);
    }
}