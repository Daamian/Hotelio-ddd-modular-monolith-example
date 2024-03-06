using Hotelio.Modules.Availability.Domain.Repository;
using Hotelio.Shared.Exception;
using MediatR;

namespace Hotelio.Modules.Availability.Application.Command.Handlers;

internal class UnBookHandler: IRequestHandler<UnBook>
{
    private readonly IResourceRepository _repository;

    public UnBookHandler(IResourceRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(UnBook command, CancellationToken cancellationToken)
    {
        var resource = await _repository.FindAsync(new Guid(command.ResourceId));
        
        if (resource is null)
        {
            throw new CommandFailedException($"Not find resource with id {command.ResourceId}");
        }
        
        resource.UnBook(new Guid(command.BookId));
    }
}