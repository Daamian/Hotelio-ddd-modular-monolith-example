namespace Hotelio.Shared.Commands;

public interface ICommandBus
{
    public Task DispatchAsync<TCommand>(TCommand commmand) where TCommand: class, ICommand;
}


