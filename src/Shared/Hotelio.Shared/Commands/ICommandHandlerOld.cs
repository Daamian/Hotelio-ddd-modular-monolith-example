using System;
namespace Hotelio.Shared.Commands;

public interface ICommandHandlerOld<in TCommand> where TCommand : class, ICommand
{
    Task HandleAsync(TCommand command);
}


