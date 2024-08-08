using System.Threading.Channels;
using Hotelio.CrossContext.Contract.Shared.Message;

namespace Hotelio.CrossContext.Infrastructure.Message;

internal class MessageChannel: IMessageChannel
{
    private readonly Channel<IMessage> _channel = Channel.CreateUnbounded<IMessage>();

    public ChannelReader<IMessage> Reader => _channel.Reader;
    public ChannelWriter<IMessage> Writer => _channel.Writer;
}