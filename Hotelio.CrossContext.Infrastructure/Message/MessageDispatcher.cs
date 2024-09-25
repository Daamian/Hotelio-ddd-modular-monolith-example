using Hotelio.CrossContext.Contract.Shared.Message;

namespace Hotelio.CrossContext.Infrastructure.Message;

internal class MessageDispatcher : IMessageDispatcher
{
    private readonly IMessageChannel _messageChannel;

    public MessageDispatcher(IMessageChannel messageChannel)
    {
        _messageChannel = messageChannel;
    }
    
    public async Task DispatchAsync(object message)
    {
        //await _messageChannel.Writer.WriteAsync(message);
    }
}