using Hotelio.CrossContext.Contract.Shared.Message;
using Hotelio.Modules.HotelManagement.Core.DAL;
using Hotelio.Modules.HotelManagement.Core.DAL.Repository;
using Hotelio.Modules.HotelManagement.Core.Model;
using Hotelio.Modules.HotelManagement.Core.Repository;
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
    private readonly Mock<IMessageDispatcher> _eventBusMock;
    private readonly IAmenityRepository _amenityRepository;
    private readonly IAmenityService _amenityService;

    public HotelServiceTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<HotelDbContext>()
            .UseSqlServer(ConfigHelper.GetSqlServerConfig().ConnectionString);
        _dbContext = new HotelDbContext(optionBuilder.Options);
        _eventBusMock = new Mock<IMessageDispatcher>();
        var repository = new HotelRepository(_dbContext, _eventBusMock.Object);
        _amenityRepository = new AmenityRepository(_dbContext);
        _amenityService = new AmenityService(_amenityRepository);
        _hotelService = new HotelService(repository, _amenityRepository);
    }

    [Fact]
    public async Task AddHotelManagementTest()
    {
        //Given
        var amenity1 = await _amenityService.AddAsync(new AmenityDto(0, "Breakfast"));
        var amenity2 = await _amenityService.AddAsync(new AmenityDto(0, "Half Board"));

        var amenities = new List<int>() { amenity1, amenity2 };
        var hotel = new HotelDto(0, "Hotel Test", amenities);
        
        //When
        var id = await _hotelService.AddAsync(hotel);
        
        //Expected
        var hotelExpected = new HotelDetailsDto(id, "Hotel Test", new List<AmenityDetailDto>()
        {
            new AmenityDetailDto(amenity1, "Breakfast"),
            new AmenityDetailDto(amenity2, "Half Board")
        });
        
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
        
        //Expected
        var hotelExpected = new HotelDetailsDto(id, "Hotel Test");
        
        //When
        await _hotelService.UpdateAsync(hotelToUpdate);
        
        //Then
        var hotelFound = await _hotelService.GetAsync(id);
        Assert.Equal(hotelExpected, hotelFound);
    }
}