using Hotelio.Modules.HotelManagement.Core.DAL;
using Hotelio.Modules.HotelManagement.Core.DAL.Repository;
using Hotelio.Modules.HotelManagement.Core.Service;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Hotelio.Shared.Event;
using Hotelio.Shared.Tests;
using Microsoft.EntityFrameworkCore;
using Moq;
using RoomRepository = Hotelio.Modules.HotelManagement.Core.DAL.Repository.RoomRepository;

namespace Hotelio.Modules.HotelManagement.Test.Integration.Service;

[Collection("Database collection")]
public class RoomServiceTest
{
    private readonly HotelService _hotelService;
    private readonly RoomService _roomService;
    private readonly HotelDbContext _dbContext;
    private readonly Mock<IEventBus> _eventBusMock;

    public RoomServiceTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<HotelDbContext>()
            .UseSqlServer(ConfigHelper.GetSqlServerConfig().ConnectionString);
        _dbContext = new HotelDbContext(optionBuilder.Options);
        _eventBusMock = new Mock<IEventBus>();
        var repository = new RoomRepository(_dbContext, _eventBusMock.Object);
        _roomService = new RoomService(repository);
        var hotelRepository = new HotelRepository(_dbContext, _eventBusMock.Object);
        _hotelService = new HotelService(hotelRepository);
    }

    [Fact]
    public async Task AddRoomTest()
    {
        //Given
        var hotel = new HotelDto(0, "Hotel Test");
        var hotelId = await _hotelService.AddAsync(hotel);
        var room = new RoomDto(0, 120, 2, 1, hotelId);
        
        //When
        var id = await _roomService.AddAsync(room);
        
        //Expected
        var roomExpected = new RoomDto(id, 120, 2, 1, hotelId);
        
        //Then
        var roomFound = await _roomService.GetAsync(id);
        Assert.Equal(roomExpected, roomFound);
    }

    [Fact]
    public async Task UpdateRoomTest()
    {
        //Given
        var hotel = new HotelDto(0, "Hotel Test");
        var hotelId = await _hotelService.AddAsync(hotel);
        var room = new RoomDto(0, 120, 2, 1, hotelId);
        var id = await _roomService.AddAsync(room);
        var roomToUpdate = new RoomDto(id, 130, 3, 2, hotelId);
        
        //When
        await _roomService.UpdateAsync(roomToUpdate);
        
        //Then
        var roomFound = await _roomService.GetAsync(id);
        Assert.Equal(roomToUpdate, roomFound);
    }
}