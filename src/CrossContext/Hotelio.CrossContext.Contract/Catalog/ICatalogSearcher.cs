namespace Hotelio.CrossContext.Contract.Catalog;

public interface ICatalogSearcher
{
    public Task<string> FindFirstRoomAvailableAsync(string hotelId, string type, DateTime startDate, DateTime endDate);
}