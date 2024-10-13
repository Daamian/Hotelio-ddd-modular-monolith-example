using Hotelio.CrossContext.Contract.Shared.Message;
using MassTransit;

namespace Hotelio.CrossContext.Infrastructure.Message;

public class MassTransitMessageDispatcher : IMessageDispatcher
{
    private readonly IBus _bus;

    public MassTransitMessageDispatcher(IBus bus)
    {
        _bus = bus;
    }
    
    public async Task DispatchAsync(object message)
    {
        await _bus.Publish(message);
    }
}