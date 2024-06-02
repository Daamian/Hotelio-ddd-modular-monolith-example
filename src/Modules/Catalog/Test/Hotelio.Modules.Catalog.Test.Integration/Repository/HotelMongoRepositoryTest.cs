using Hotelio.Modules.Catalog.Core.Model;
using Hotelio.Modules.Catalog.Core.Repository;
using Hotelio.Modules.Catalog.Core.Repository.Settings;
using Microsoft.Extensions.Options;
using Mongo2Go;
using MongoDB.Driver;

namespace Hotelio.Modules.Catalog.Test.Integration.Repository;

public class HotelMongoRepositoryTest
{
    private readonly MongoDbRunner _mongoRunner;
    private readonly IMongoDatabase _database;
    private readonly IHotelRepository _hotelRepository;

    public HotelMongoRepositoryTest()
    {
        _mongoRunner = MongoDbRunner.Start();
        var settings = new HotelStoreDBSettings
        {
            ConnectionString = _mongoRunner.ConnectionString,
            DatabaseName = "IntegrationTestsDB",
            HotelsCollectionName = "Hotels"
        };

        var options = Options.Create(settings);
        _hotelRepository = new HotelMongoRepository(options);

        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }
    

    [Fact]
    public async Task AddAsync_ShouldAddHotel()
    {
        var hotel = new Hotel { Id = "1", Name = "Test Hotel", Rooms = new List<Room>() };
        await _hotelRepository.AddAsync(hotel);

        var retrievedHotel = await _hotelRepository.FindAsync(hotel.Id);

        Assert.NotNull(retrievedHotel);
        Assert.Equal(hotel.Name, retrievedHotel.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateHotel()
    {
        var hotel = new Hotel { Id = "2", Name = "Old Hotel Name", Rooms = new List<Room>() };
        await _hotelRepository.AddAsync(hotel);

        hotel.Name = "Updated Hotel Name";
        await _hotelRepository.UpdateAsync(hotel);

        var updatedHotel = await _hotelRepository.FindAsync(hotel.Id);

        Assert.NotNull(updatedHotel);
        Assert.Equal(hotel.Name, updatedHotel.Name);
    }

    [Fact]
    public async Task FindByRoomAsync_ShouldReturnHotel()
    {
        var roomId = "Room1";
        var hotel = new Hotel
        {
            Id = "3",
            Name = "Test Hotel",
            Rooms = new List<Room> { new Room { Id = roomId, MaxGuests = 2, Type = "Deluxe", Reservations = new List<Reservation>() } }
        };
        await _hotelRepository.AddAsync(hotel);

        var retrievedHotel = await _hotelRepository.FindByRoomAsync(roomId);

        Assert.NotNull(retrievedHotel);
        Assert.Equal(hotel.Id, retrievedHotel.Id);
    }
    
    public void Dispose()
    {
        _mongoRunner.Dispose();
    }
}