using Hotelio.Modules.Availability.Application.ReadModel;
using MediatR;

namespace Hotelio.Modules.Availability.Application.Query.Handler;

internal class GetFirstAvailableResourceInDateRangeHandler : IRequestHandler<GetFirstAvailableResourceInDateRange, Resource?>
{
    private readonly IResourceStorage _storage;

    public GetFirstAvailableResourceInDateRangeHandler(IResourceStorage storage)
    {
        _storage = storage;
    }
    
    public async Task<Resource?> Handle(GetFirstAvailableResourceInDateRange request, CancellationToken cancellationToken)
    {
        return await _storage.FindFirstAvailableInDatesAsync(
            request.Group,
            request.Type,
            request.StarDate,
            request.EndDate);
    }
}