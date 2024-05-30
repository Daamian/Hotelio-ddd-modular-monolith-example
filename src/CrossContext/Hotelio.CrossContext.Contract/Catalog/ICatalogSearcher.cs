namespace Hotelio.CrossContext.Contract.Catalog;

public interface ICatalogSearcher
{
    public Task<string> FindFirstAvailableAsync(string hotelId, string type, DateTime startDate, DateTime endDate);
}