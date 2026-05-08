namespace Hotelio.CrossContext.Contract.Availability;

public interface IAvailability
{
    public Task CreateResource(string resourceId);
    
    public Task BookAsync(
        string resourceId,
        string ownerId, 
        DateTime startDate,
        DateTime endDate);

    public Task UnBookAsync(string resourceId, string ownerId);
    
}