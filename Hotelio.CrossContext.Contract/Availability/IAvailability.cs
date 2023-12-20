namespace Hotelio.CrossContext.Contract.Availability;

public interface IAvailability
{
    public Task BookFirstAvailableAsync(
        string group, 
        int type, 
        string ownerId, 
        DateTime starDate, 
        DateTime endDate);

    public Task UnBookAsync(string resourceId, string ownerId);
}