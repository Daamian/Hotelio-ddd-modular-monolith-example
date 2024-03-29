using Hotelio.CrossContext.Contract.Availability;
using Hotelio.Modules.Availability.Api.CrossContext;
using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Modules.Availability.Infrastructure.ReadModel;
using Hotelio.Modules.Availability.Infrastructure.Repository;
using Hotelio.Shared.Event;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Hotelio.Modules.Availability.Test.Integration.CrossContext;

public class AvailabilityServiceTest
{
    private readonly SqlServerResourceStorage _storage = new SqlServerResourceStorage("Server=localhost,1433;Database=tests;User=sa;Password=Your_password123;TrustServerCertificate=True");
    private readonly ResourceDbContext _dbContext;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly EfResourceRepository _resourceRepository;
    private readonly IAvailability _availability;
    
    public AvailabilityServiceTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<ResourceDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=tests;User=sa;Password=Your_password123;TrustServerCertificate=True");
        _dbContext = new ResourceDbContext(optionBuilder.Options);
        _resourceRepository = new EfResourceRepository(_dbContext, _eventBusMock.Object);
    }
    
    [Fact]
    public async void TestBook()
    {
        //TODO:
        //1. Prepare and persist resource with books
        /*var resourceId = Guid.NewGuid();
        var resource = Resource.Create(resourceId, "group-1", 1, true);
        resource.Book("owner-1", new DateTime(2024, 1, 1), new DateTime(2024, 1, 5));
        resource.Book("owner-2", new DateTime(2024, 1, 10), new DateTime(2024, 1, 14));
        await _resourceRepository.AddAsync(resource);*/
        
        //2. Try to book in available dates in 6.01 - 10.01
        
        //var resourceFound = await _storage.FindFirstAvailableInDatesAsync("group", 1, startDate, endDate);
        //TODO how to test AvailabilityService ???
        Assert.True(true);
    }
}