using System.Threading.Channels;
using Hotelio.CrossContext.Contract.Shared.Message;

namespace Hotelio.CrossContext.Infrastructure.Message;

internal interface IMessageChannel
{
    ChannelReader<IMessage> Reader { get; }
    ChannelWriter<IMessage> Writer { get; }
}