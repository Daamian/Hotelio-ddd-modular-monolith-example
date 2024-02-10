namespace Hotelio.CrossContext.Contract.Availability;

using Hotelio.CrossContext.Contract.Availability.Exception;

public interface IAvailability
{
    /// <exception cref="ResourceIsNotAvailableException">Throws when resource is not available in specific dates</exception>
    public Task BookFirstAvailableAsync(
        string group, 
        int type, 
        string ownerId, 
        DateTime starDate, 
        DateTime endDate);

    public Task UnBookAsync(string resourceId, string ownerId);
}