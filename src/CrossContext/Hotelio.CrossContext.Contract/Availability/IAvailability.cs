namespace Hotelio.CrossContext.Contract.Availability;

using Exception;

public interface IAvailability
{
    public Task CreateResource(string resourceId);
    
    /// <exception cref="ResourceIsNotAvailableException">Throws when resource is not available in specific dates</exception>
    public Task BookAsync(
        string resourceId,
        string ownerId, 
        DateTime starDate, 
        DateTime endDate);

    public Task UnBookAsync(string resourceId, string ownerId);
    
}