using Hotelio.Modules.HotelManagement.Core.DAL;
using Hotelio.Modules.HotelManagement.Core.DAL.Repository;
using Hotelio.Modules.HotelManagement.Core.Service;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Hotelio.Shared.Tests;
using Microsoft.EntityFrameworkCore;

namespace Hotelio.Modules.HotelManagement.Test.Integration.Service;

[Collection("Database collection")]
public class HotelServiceTest
{
    private readonly HotelService _hotelService;
    private readonly HotelDbContext _dbContext;

    public HotelServiceTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<HotelDbContext>()
            .UseSqlServer(ConfigHelper.GetSqlServerConfig().ConnectionString);
        _dbContext = new HotelDbContext(optionBuilder.Options);
        var repository = new HotelRepository(_dbContext);
        _hotelService = new HotelService(repository);
    }

    [Fact]
    public void AddHotelManagementTest()
    {
        //Given
        var hotel = new HotelDto(0, "Hotel Test");
        
        
        //When
        var id = _hotelService.Add(hotel);
        
        //Expected
        var hotelExpected = new HotelDto(id, "Hotel Test");
        
        //Then
        var hotelFound = _hotelService.Get(id);
        Assert.Equal(hotelExpected, hotelFound);
    }

    [Fact]
    public void UpdateHotelTest()
    {
        
        //Given
        var hotel = new HotelDto(0, "Hotel Test");
        var id = _hotelService.Add(hotel);
        var hotelToUpdate = new HotelDto(id, "New name");
        
        //When
        _hotelService.Update(hotelToUpdate);
        
        //Then
        var hotelFound = _hotelService.Get(id);
        Assert.Equal(hotelToUpdate, hotelFound);
    }
}