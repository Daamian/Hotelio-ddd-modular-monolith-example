using MediatR;

namespace Hotelio.Shared.Queries;

public class MediatRQueryBus : IQueryBus
{
    private readonly IMediator _mediator;
    
    public MediatRQueryBus(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        return await _mediator.Send(query);
    }
}