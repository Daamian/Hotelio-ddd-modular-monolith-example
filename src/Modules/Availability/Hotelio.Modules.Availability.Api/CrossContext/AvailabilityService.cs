using Hotelio.CrossContext.Contract.Availability;
using Hotelio.CrossContext.Contract.Availability.Event;
using Hotelio.CrossContext.Contract.Availability.Exception;
using Hotelio.CrossContext.Contract.Shared.Message;
using Hotelio.Modules.Availability.Application.Command;
using Hotelio.Modules.Availability.Domain.Repository;
using BookCommand = Hotelio.Modules.Availability.Application.Command.Book;
using Hotelio.Shared.Commands;
using Hotelio.Shared.Exception;

namespace Hotelio.Modules.Availability.Api.CrossContext;

internal class AvailabilityService: IAvailability
{
    private readonly ICommandBus _commandBus;
    private readonly IResourceRepository _repository;
    private readonly IMessageDispatcher _messageDispatcher;

    public AvailabilityService(ICommandBus commandBus, IResourceRepository repository, IMessageDispatcher messageDispatcher)
    {
        _commandBus = commandBus;
        _repository = repository;
        _messageDispatcher = messageDispatcher;
    }

    public async Task CreateResource(string resourceId)
    {
        await _commandBus.DispatchAsync(new Create(Guid.NewGuid(), resourceId));
    }

    public async Task BookAsync(string resourceId, string ownerId, DateTime starDate, DateTime endDate)
    {
        var resource = await _repository.FindByExternalIdAsync(resourceId);
        
        if (resource is null)
        {
            await _messageDispatcher.DispatchAsync(new ResourceBookRejected(resourceId, ownerId, starDate, endDate, $"Resource is not found with id {resourceId} "));
            return;
        }

        try
        {
            await _commandBus.DispatchAsync(new BookCommand(resource.Id, ownerId, starDate, endDate));
        }
        catch (Exception e) when (e is DomainException or CommandFailedException)
        {
            await _messageDispatcher.DispatchAsync(new ResourceBookRejected(resourceId, ownerId, starDate, endDate, e.Message));
        }
    }

    public async Task UnBookAsync(string resourceId, string ownerId)
    {
        //TODO bookId against ownerId ???
        await _commandBus.DispatchAsync(new UnBook(resourceId, ownerId));
    }
}