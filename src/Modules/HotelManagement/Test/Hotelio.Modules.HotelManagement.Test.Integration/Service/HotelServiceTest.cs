using Hotelio.Modules.HotelManagement.Core.DAL;
using Hotelio.Modules.HotelManagement.Core.DAL.Repository;
using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Service;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Hotelio.Shared.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace Hotelio.Modules.HotelManagement.Test.Integration.Service;

public class HotelServiceTest : IDisposable
{
    private readonly HotelService _hotelService;
    private readonly HotelDbContext _dbContext;
    private readonly HotelRepository _repository;
    
    public HotelServiceTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<HotelDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=tests;User=sa;Password=Your_password123;TrustServerCertificate=True");
        _dbContext = new HotelDbContext(optionBuilder.Options);
        _repository = new HotelRepository(_dbContext);
        _hotelService = new HotelService(_repository);
    }

    [Fact]
    public void AddHotelManagementTest()
    {
        _dbContext.Database.EnsureCreated();
        
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
        _dbContext.Database.EnsureCreated();
        
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

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}