using Hotelio.CrossContext.Contract.Catalog;
using Hotelio.CrossContext.Contract.Catalog.Exception;
using Hotelio.Modules.Catalog.Core.Repository;

namespace Hotelio.Modules.Catalog.Api.CrossContext.Service;

internal class CatalogSearcher: ICatalogSearcher
{
    private readonly IHotelRepository _repository;

    public CatalogSearcher(IHotelRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<string> FindFirstRoomAvailableAsync(string hotelId, string type, DateTime startDate, DateTime endDate)
    {
        var room = await _repository.FindFirstRoomAvailableAsync(hotelId, type, startDate, endDate);

        if (room is null)
        {
            throw new NotFoundAvailableRoomException($"Not found available rooms for {hotelId} in specific dates");
        }

        return room.Id;
    }
}