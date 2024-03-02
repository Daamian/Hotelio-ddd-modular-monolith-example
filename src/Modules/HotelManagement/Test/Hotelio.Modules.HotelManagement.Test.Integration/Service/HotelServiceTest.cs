using Hotelio.Modules.HotelManagement.Core.DAL;
using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Service;
using Hotelio.Shared.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace Hotelio.Modules.HotelManagement.Test.Integration.Service;

public class HotelServiceTest
{
    private readonly HotelService _hotelService;
    private readonly HotelDbContext _dbContext;
    
    public HotelServiceTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<HotelDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=tests;User=sa;Password=Your_password123;");
        _dbContext = new HotelDbContext(optionBuilder.Options);
        _hotelService = new HotelService(_dbContext);
    }

    [Fact]
    public void AddHotelManagementTest()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var rooms = new List<Room>()
        {
            new Room() { Number = 1, MaxGuests = 2, Type = RoomType.Standard},
            new Room() { Number = 2, MaxGuests = 4, Type = RoomType.Superior}
        };
        
        var hotel = new Hotel() { Name = "Test hotel", Rooms = rooms};
        
        //When
        _hotelService.Add(hotel);
        
        //Then
        var hotelFound = _hotelService.Find(hotel.Id);
        Assert.Equal(hotel, hotelFound);
    }

    [Fact]
    public void UpdateHotelTest()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        
        //When
        var hotelToUpdate = _hotelService.Find(1);
        hotelToUpdate.Rooms.Add(new Room() { Number = 1, MaxGuests = 2, Type = RoomType.Standard});
        _hotelService.Update(hotelToUpdate);
        
        //Then
        var hotelFound = _hotelService.Find(1);
        Assert.Equal(hotelToUpdate, hotelFound);
    }
}