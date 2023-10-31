using MassTransit;

namespace Hotelio.Shared.Commands;

public interface ICommandHandlerSecond<in TCommand>: IConsumer<TCommand> where TCommand: class, ICommand
{
    
}