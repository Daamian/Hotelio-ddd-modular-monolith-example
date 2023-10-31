using MediatR;

namespace Hotelio.Shared.Commands;

public class MediatRCommandBus: ICommandBus
{
    private readonly IMediator _mediator;

    public MediatRCommandBus(IMediator _mediator)
    {
        this._mediator = _mediator;
    }
    
    public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand
    {
        await this._mediator.Send(command);
    }
}