using Hotelio.Modules.Availability.Domain.Repository;
using Hotelio.Shared.Exception;
using MediatR;

namespace Hotelio.Modules.Availability.Application.Command.Handlers;

internal sealed class BookHandler: IRequestHandler<Book>
{
    private readonly IResourceRepository _repository;

    public BookHandler(IResourceRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(Book command, CancellationToken cancellationToken)
    {
        var resource = await _repository.FindAsync(command.ResourceId);
        
        if (resource is null)
        {
            throw new CommandFailedException($"Not find resource with id {command.ResourceId}");
        }
        
        resource.Book(command.OwnerId, command.StarDate, command.EndDate); 
        await _repository.UpdateAsync(resource);
    }
}