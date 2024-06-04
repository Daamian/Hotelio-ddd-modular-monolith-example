using Hotelio.Modules.HotelManagement.Core.DAL;
using Hotelio.Modules.HotelManagement.Core.DAL.Repository;
using Hotelio.Modules.HotelManagement.Core.Service;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Hotelio.Shared.Event;
using Hotelio.Shared.Tests;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Hotelio.Modules.HotelManagement.Test.Integration.Service;

[Collection("Database collection")]
public class HotelServiceTest
{
    private readonly HotelService _hotelService;
    private readonly HotelDbContext _dbContext;
    private readonly Mock<IEventBus> _eventBusMock;

    public HotelServiceTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<HotelDbContext>()
            .UseSqlServer(ConfigHelper.GetSqlServerConfig().ConnectionString);
        _dbContext = new HotelDbContext(optionBuilder.Options);
        _eventBusMock = new Mock<IEventBus>();
        var repository = new HotelRepository(_dbContext, _eventBusMock.Object);
        _hotelService = new HotelService(repository);
    }

    [Fact]
    public async Task AddHotelManagementTest()
    {
        //Given
        var hotel = new HotelDto(0, "Hotel Test");
        
        
        //When
        var id = await _hotelService.AddAsync(hotel);
        
        //Expected
        var hotelExpected = new HotelDto(id, "Hotel Test");
        
        //Then
        var hotelFound = await _hotelService.GetAsync(id);
        Assert.Equal(hotelExpected, hotelFound);
    }

    [Fact]
    public async Task UpdateHotelTest()
    {
        
        //Given
        var hotel = new HotelDto(0, "Hotel Test");
        var id = await _hotelService.AddAsync(hotel);
        var hotelToUpdate = new HotelDto(id, "New name");
        
        //When
        await _hotelService.UpdateAsync(hotelToUpdate);
        
        //Then
        var hotelFound = await _hotelService.GetAsync(id);
        Assert.Equal(hotelToUpdate, hotelFound);
    }
}