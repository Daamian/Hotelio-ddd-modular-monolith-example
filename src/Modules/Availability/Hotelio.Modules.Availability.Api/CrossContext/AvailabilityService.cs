using Hotelio.CrossContext.Contract.Availability;
using Hotelio.CrossContext.Contract.Availability.Exception;
using Hotelio.Modules.Availability.Application.Command;
using BookCommand = Hotelio.Modules.Availability.Application.Command.Book;
using Hotelio.Modules.Availability.Application.ReadModel;
using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Availability.Api.CrossContext;

internal class AvailabilityService: IAvailability
{
    private readonly IResourceStorage _resourceStorage;
    private readonly ICommandBus _commandBus;

    public AvailabilityService(IResourceStorage resourceStorage, ICommandBus commandBus)
    {
        _resourceStorage = resourceStorage;
        _commandBus = commandBus;
    }

    public async Task BookFirstAvailableAsync(string group, int type, string ownerId, DateTime starDate, DateTime endDate)
    {
        var resource = _resourceStorage.FindFirstAvailableInDates(group, type, starDate, endDate);

        if (resource is null)
        {
            throw new ResourceIsNotAvailableException($"Resource of group {group} and type {type} in specific date range");
        }

        //TODO what if command handler throws exception ???
        await this._commandBus.DispatchAsync(new BookCommand(resource.Id, ownerId, starDate, endDate));
    }

    public async Task UnBookAsync(string resourceId, string ownerId)
    {
        //TODO bookId against ownerId ???
        await this._commandBus.DispatchAsync(new UnBook(resourceId, ownerId));
    }
}