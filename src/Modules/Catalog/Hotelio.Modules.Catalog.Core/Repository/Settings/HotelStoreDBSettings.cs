namespace Hotelio.Modules.Catalog.Core.Repository.Settings;

internal class HotelStoreDBSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string HotelsCollectionName { get; set; } = null!;
}