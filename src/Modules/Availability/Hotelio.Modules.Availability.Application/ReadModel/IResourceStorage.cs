namespace Hotelio.Modules.Availability.Application.ReadModel;

internal interface IResourceStorage
{
    public Resource? FindFirstAvailableInDates(string group, int type, DateTime startDate, DateTime endDate);
}