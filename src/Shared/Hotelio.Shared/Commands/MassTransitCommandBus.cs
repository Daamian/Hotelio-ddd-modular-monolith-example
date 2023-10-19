using MassTransit;

namespace Hotelio.Shared.Commands;

public class MassTransitCommandBus : ICommandBus
{
    private readonly IBus _bus;

    public MassTransitCommandBus(IBus bus)
    {
        this._bus = bus;
    }
    
    public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand
    {
        await this._bus.Publish(command);
    }
}