namespace Hotelio.CrossContext.Contract.Availability;

using Exception;

public interface IAvailability
{
    public Task CreateResource(string resourceId, string groupId, int type);
    
    /// <exception cref="ResourceIsNotAvailableException">Throws when resource is not available in specific dates</exception>
    public Task BookFirstAvailableAsync(
        string group, 
        int type, 
        string ownerId, 
        DateTime starDate, 
        DateTime endDate);

    public Task UnBookAsync(string resourceId, string ownerId);
    
}