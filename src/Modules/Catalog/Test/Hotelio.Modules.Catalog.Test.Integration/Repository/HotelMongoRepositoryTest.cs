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

    [Fact]
    public async Task FindFirstAvailableWithOnePossibleRoom()
    {
        var hotel = new Hotel
        {
            Id = "3",
            Name = "Test Hotel",
            Rooms = new List<Room> { 
                new Room()
                {
                    Id = "Room1", MaxGuests = 2, Type = "Deluxe", Reservations = new List<Reservation>()
                    {
                        new Reservation() { StartDate = new DateTime(2024,1 , 10), StopDate = new DateTime(2024,1, 20)},
                        new Reservation() { StartDate = new DateTime(2024,2 , 10), StopDate = new DateTime(2024,2, 20)},
                        new Reservation() { StartDate = new DateTime(2024,3 , 10), StopDate = new DateTime(2024,3, 20)}
                    }
                },
                new Room()
                {
                    Id = "Room2", MaxGuests = 2, Type = "Deluxe", Reservations = new List<Reservation>()
                    {
                        new Reservation() { StartDate = new DateTime(2024,1 , 1), StopDate = new DateTime(2024,1, 5)},
                        new Reservation() { StartDate = new DateTime(2024,2 , 25), StopDate = new DateTime(2024,2, 27)},
                        new Reservation() { StartDate = new DateTime(2024,3 , 15), StopDate = new DateTime(2024,3, 25)}
                    }
                }
            }
        };
        
        await _hotelRepository.AddAsync(hotel);

        var room = await _hotelRepository.FindFirstRoomAvailableAsync(
            "3", 
            "Deluxe",
            new DateTime(2024,1 , 5),
            new DateTime(2024,1, 21)
            );
        
        Assert.Equal("Room2", room.Id);
    }
    
    [Fact]
    public async Task FindFirstAvailableWithManyPossibleRoom()
    {
        var hotel = new Hotel
        {
            Id = "3",
            Name = "Test Hotel",
            Rooms = new List<Room> { 
                new Room()
                {
                    Id = "Room1", MaxGuests = 2, Type = "Deluxe", Reservations = new List<Reservation>()
                    {
                        new() { StartDate = new DateTime(2024,1 , 3), StopDate = new DateTime(2024,1, 4)},
                        new() { StartDate = new DateTime(2024,2 , 10), StopDate = new DateTime(2024,2, 20)},
                        new() { StartDate = new DateTime(2024,3 , 10), StopDate = new DateTime(2024,3, 20)}
                    }
                },
                new Room()
                {
                    Id = "Room2", MaxGuests = 2, Type = "Deluxe", Reservations = new List<Reservation>()
                    {
                        new() { StartDate = new DateTime(2024,1 , 1), StopDate = new DateTime(2024,1, 5)},
                        new() { StartDate = new DateTime(2024,2 , 25), StopDate = new DateTime(2024,2, 27)},
                        new() { StartDate = new DateTime(2024,3 , 15), StopDate = new DateTime(2024,3, 25)}
                    }
                }
            }
        };
        
        await _hotelRepository.AddAsync(hotel);

        var room = await _hotelRepository.FindFirstRoomAvailableAsync(
            "3", 
            "Deluxe",
            new DateTime(2024,1 , 5),
            new DateTime(2024,1, 21)
        );
        
        Assert.Equal("Room1", room.Id);
    }
    
    [Fact]
    public async Task FindFirstAvailableWithNoPossibleRooms()
    {
        var hotel = new Hotel
        {
            Id = "3",
            Name = "Test Hotel",
            Rooms = new List<Room> { 
                new Room()
                {
                    Id = "Room1", MaxGuests = 2, Type = "Deluxe", Reservations = new List<Reservation>()
                    {
                        new() { StartDate = new DateTime(2024,1 , 1), StopDate = new DateTime(2024,1, 21)},
                        new() { StartDate = new DateTime(2024,2 , 10), StopDate = new DateTime(2024,2, 20)},
                        new() { StartDate = new DateTime(2024,3 , 10), StopDate = new DateTime(2024,3, 20)}
                    }
                },
                new Room()
                {
                    Id = "Room2", MaxGuests = 2, Type = "Deluxe", Reservations = new List<Reservation>()
                    {
                        new() { StartDate = new DateTime(2024,1 , 1), StopDate = new DateTime(2024,1, 6)},
                        new() { StartDate = new DateTime(2024,2 , 25), StopDate = new DateTime(2024,2, 27)},
                        new() { StartDate = new DateTime(2024,3 , 15), StopDate = new DateTime(2024,3, 25)}
                    }
                }
            }
        };
        
        await _hotelRepository.AddAsync(hotel);

        var room = await _hotelRepository.FindFirstRoomAvailableAsync(
            "3", 
            "Deluxe",
            new DateTime(2024,1 , 5),
            new DateTime(2024,1, 21)
        );
        
        Assert.Null(room);
    }
    
    public void Dispose()
    {
        _mongoRunner.Dispose();
    }
}