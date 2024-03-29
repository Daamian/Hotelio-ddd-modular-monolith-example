using MediatR;

namespace Hotelio.Shared.Commands;

public class MediatRCommandBus: ICommandBus
{
    private readonly IMediator _mediator;

    public MediatRCommandBus(IMediator mediator)
    {
        this._mediator = mediator;
    }
    
    public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand
    {
        await this._mediator.Send(command);
    }
}