namespace Hotelio.Modules.Availability.Api.Services;

public interface IAvailabilityService
{
    public Task BookFirstAvailableAsync(
        string group, 
        int type, 
        string ownerId, 
        DateTime starDate, 
        DateTime endDate);

    public Task UnBookAsync(string resourceId, string ownerId);
}