using Hotelio.Modules.HotelManagement.Core.DAL;
using Hotelio.Modules.HotelManagement.Core.DAL.Repository;
using Hotelio.Modules.HotelManagement.Core.Service;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Hotelio.Shared.Tests;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.HotelManagement.Test.Integration.Service;

public class RoomServiceTest: IDisposable
{
    private readonly HotelService _hotelService;
    private readonly RoomService _roomService;
    private readonly HotelDbContext _dbContext;

    public RoomServiceTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<HotelDbContext>()
            .UseSqlServer(ConfigHelper.GetSqlServerConfig().ConnectionString);
        _dbContext = new HotelDbContext(optionBuilder.Options);
        var repository = new RoomRepository(_dbContext);
        _roomService = new RoomService(repository);
        var hotelRepository = new HotelRepository(_dbContext);
        _hotelService = new HotelService(hotelRepository);
    }

    [Fact]
    public void AddRoomTest()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var hotel = new HotelDto(0, "Hotel Test");
        var hotelId = _hotelService.Add(hotel);
        var room = new RoomDto(0, 120, 2, 1, hotelId);
        
        //When
        var id = _roomService.Add(room);
        
        //Expected
        var roomExpected = new RoomDto(id, 120, 2, 1, 1);
        
        //Then
        var roomFound = _roomService.Get(id);
        Assert.Equal(roomExpected, roomFound);
    }

    [Fact]
    public void UpdateRoomTest()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var hotel = new HotelDto(0, "Hotel Test");
        var hotelId = _hotelService.Add(hotel);
        var room = new RoomDto(0, 120, 2, 1, hotelId);
        var id = _roomService.Add(room);
        var roomToUpdate = new RoomDto(id, 130, 3, 2, hotelId);
        
        //When
        _roomService.Update(roomToUpdate);
        
        //Then
        var roomFound = _roomService.Get(id);
        Assert.Equal(roomToUpdate, roomFound);
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}