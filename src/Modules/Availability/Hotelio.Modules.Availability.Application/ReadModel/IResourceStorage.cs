namespace Hotelio.Modules.Availability.Application.ReadModel;

internal interface IResourceStorage
{
    public Task<Resource?> FindFirstAvailableInDatesAsync(string group, int type, DateTime startDate, DateTime endDate);
}