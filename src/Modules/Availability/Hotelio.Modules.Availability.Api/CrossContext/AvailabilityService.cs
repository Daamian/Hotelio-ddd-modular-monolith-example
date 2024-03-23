using Hotelio.CrossContext.Contract.Availability;
using Hotelio.CrossContext.Contract.Availability.Exception;
using Hotelio.Modules.Availability.Application.Command;
using Hotelio.Modules.Availability.Application.Query;
using BookCommand = Hotelio.Modules.Availability.Application.Command.Book;
using Hotelio.Shared.Commands;
using Hotelio.Shared.Queries;

namespace Hotelio.Modules.Availability.Api.CrossContext;

internal class AvailabilityService: IAvailability
{
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;

    public AvailabilityService(IQueryBus queryBus, ICommandBus commandBus)
    {
        _queryBus = queryBus;
        _commandBus = commandBus;
    }

    public async Task BookFirstAvailableAsync(string group, int type, string ownerId, DateTime starDate, DateTime endDate)
    {
        var resource = await _queryBus.QueryAsync(
            new GetFirstAvailableResourceInDateRange(group, type, starDate, endDate));

        if (resource is null)
        {
            throw new ResourceIsNotAvailableException(
                $"Resource of group {group} and type {type} in specific date range");
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