using Hotelio.CrossContext.Contract.Availability;
using Hotelio.CrossContext.Contract.Availability.Exception;
using Hotelio.Modules.Availability.Application.Command;
using Hotelio.Modules.Availability.Domain.Repository;
using BookCommand = Hotelio.Modules.Availability.Application.Command.Book;
using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Availability.Api.CrossContext;

internal class AvailabilityService: IAvailability
{
    private readonly ICommandBus _commandBus;
    private readonly IResourceRepository _repository;

    public AvailabilityService(ICommandBus commandBus, IResourceRepository repository)
    {
        _commandBus = commandBus;
        _repository = repository;
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
            throw new ResourceNotFoundException($"Resource is not found ");
        }

        //TODO what if command handler throws exception ???
        await _commandBus.DispatchAsync(new BookCommand(resource.Id, ownerId, starDate, endDate));
    }

    public async Task UnBookAsync(string resourceId, string ownerId)
    {
        //TODO bookId against ownerId ???
        await _commandBus.DispatchAsync(new UnBook(resourceId, ownerId));
    }
}