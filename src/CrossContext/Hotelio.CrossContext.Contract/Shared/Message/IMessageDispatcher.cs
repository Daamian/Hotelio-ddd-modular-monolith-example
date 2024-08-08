namespace Hotelio.CrossContext.Contract.Shared.Message;

public interface IMessageDispatcher
{
    Task DispatchAsync(IMessage message);
}