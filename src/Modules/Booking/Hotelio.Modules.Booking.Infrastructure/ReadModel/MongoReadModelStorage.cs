using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Application.ReadModel;
using Hotelio.Modules.Booking.Infrastructure.ReadModel.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hotelio.Modules.Booking.Infrastructure.ReadModel;

internal class MongoReadModelStorage: IReadModelStorage
{
    private readonly IMongoCollection<Reservation> _collection;
    
    public MongoReadModelStorage(IOptions<ReservationStoreDatabaseSettings> reservationStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            reservationStoreDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(
            reservationStoreDatabaseSettings.Value.DatabaseName);
        _collection = mongoDatabase.GetCollection<Reservation>(
            reservationStoreDatabaseSettings.Value.ReservationsCollectionName);
    }

    public async Task<Reservation> FindAsync(Guid reservationId) => await _collection.Find(r => r.Id == reservationId.ToString()).SingleOrDefaultAsync();

    public async Task SaveAsync(Reservation reservation) => await _collection.ReplaceOneAsync(r => r.Id == reservation.Id, reservation, new ReplaceOptions { IsUpsert = true });
    
    
}