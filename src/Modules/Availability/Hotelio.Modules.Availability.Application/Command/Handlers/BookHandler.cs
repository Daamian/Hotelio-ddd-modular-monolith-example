using Hotelio.Modules.Availability.Domain.Repository;
using Hotelio.Shared.Exception;
using MediatR;

namespace Hotelio.Modules.Availability.Application.Command.Handlers;

internal sealed class BookHandler: IRequestHandler<Book>
{
    private IResourceRepository _repository;

    public BookHandler(IResourceRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(Book command, CancellationToken cancellationToken)
    {
        var resource = _repository.Find(new Guid(command.ResourceId));
        
        if (resource is null)
        {
            throw new CommandFailedException($"Not find resource with id {command.ResourceId}");
        }
        
        //TODO try domain exception and dispatch events ???
        resource.Book(command.OwnerId, command.StarDate, command.EndDate);
        this._repository.Update(resource);

        //return Task.CompletedTask;
    }
}