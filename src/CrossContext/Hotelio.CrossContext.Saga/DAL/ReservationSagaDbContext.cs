using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.CrossContext.Saga.DAL;

public class ReservationSagaDbContext : MassTransit.EntityFrameworkCoreIntegration.SagaDbContext
{
    public ReservationSagaDbContext(DbContextOptions options) : base(options) { }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get { yield return new ReservationStateMap(); }
    }
}