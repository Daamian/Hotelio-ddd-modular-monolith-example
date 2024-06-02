using Hotelio.Modules.Catalog.Core.Model;
using Hotelio.Modules.Catalog.Core.Repository.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hotelio.Modules.Catalog.Core.Repository;

internal class HotelMongoRepository: IHotelRepository
{
    private readonly IMongoCollection<Hotel> _collection;
    
    public HotelMongoRepository(IOptions<HotelStoreDBSettings> hotelStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            hotelStoreDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(
            hotelStoreDatabaseSettings.Value.DatabaseName);
        _collection = mongoDatabase.GetCollection<Hotel>(
            hotelStoreDatabaseSettings.Value.HotelsCollectionName);
    }
    
    public async Task AddAsync(Hotel hotel)
    {
        await _collection.InsertOneAsync(hotel);
    }

    public async Task UpdateAsync(Hotel hotel)
    {
        await _collection.ReplaceOneAsync(h => h.Id == hotel.Id, hotel);
    }

    public async Task<Hotel?> FindAsync(string id)
    {
        return await _collection.Find(h => h.Id == id).SingleOrDefaultAsync();
    }

    public async Task<Hotel> FindByRoomAsync(string roomId)
    {
        return await _collection.Find(h => h.Rooms.Exists(r => r.Id == roomId)).SingleOrDefaultAsync();
    }
}