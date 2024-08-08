using Hotelio.CrossContext.Contract.Shared.Message;
using Hotelio.Modules.HotelManagement.Core.DAL;
using Hotelio.Modules.HotelManagement.Core.DAL.Repository;
using Hotelio.Modules.HotelManagement.Core.Repository;
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
    private readonly RoomTypeService _roomTypeService;
    private readonly RoomService _roomService;
    private readonly HotelDbContext _dbContext;
    private readonly Mock<IMessageDispatcher> _eventBusMock;
    private readonly IAmenityRepository _amenityRepository;

    public RoomServiceTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<HotelDbContext>()
            .UseSqlServer(ConfigHelper.GetSqlServerConfig().ConnectionString);
        _dbContext = new HotelDbContext(optionBuilder.Options);
        _eventBusMock = new Mock<IMessageDispatcher>();
        var repository = new RoomRepository(_dbContext, _eventBusMock.Object);
        _roomService = new RoomService(repository);
        var hotelRepository = new HotelRepository(_dbContext, _eventBusMock.Object);
        _amenityRepository = new AmenityRepository(_dbContext);
        _hotelService = new HotelService(hotelRepository, _amenityRepository);
        _roomTypeService = new RoomTypeService(new RoomTypeRepository(_dbContext));
    }

    [Fact]
    public async Task AddRoomTest()
    {
        //Given
        var roomType = new RoomTypeDto(0, "Superior", 2, 1);
        var roomTypeId = await _roomTypeService.AddAsync(roomType);
        var hotel = new HotelDto(0, "Hotel Test");
        var hotelId = await _hotelService.AddAsync(hotel);
        var room = new RoomDto(0, 120, roomTypeId, hotelId);
        
        //When
        var id = await _roomService.AddAsync(room);
        
        //Expected
        var roomExpected = new RoomDto(id, 120, roomTypeId, hotelId);

        await _hotelService.GetAsync(hotelId);
        //Then
        var roomFound = await _roomService.GetAsync(id);
        Assert.Equal(roomExpected, roomFound);
    }

    [Fact]
    public async Task UpdateRoomTest()
    {
        //Given
        var roomType = new RoomTypeDto(0, "Superior", 2, 1);
        var roomTypeId = await _roomTypeService.AddAsync(roomType);
        var roomType2 = new RoomTypeDto(0, "Superior 222", 2, 1);
        var roomTypeId2 = await _roomTypeService.AddAsync(roomType2);
        var hotel = new HotelDto(0, "Hotel Test");
        var hotelId = await _hotelService.AddAsync(hotel);
        var room = new RoomDto(0, 120, roomTypeId, hotelId);
        var id = await _roomService.AddAsync(room);
        var roomToUpdate = new RoomDto(id, 130, roomTypeId2, hotelId);
        
        //When
        await _roomService.UpdateAsync(roomToUpdate);
        
        //Then
        var roomFound = await _roomService.GetAsync(id);
        Assert.Equal(roomToUpdate, roomFound);
    }
}