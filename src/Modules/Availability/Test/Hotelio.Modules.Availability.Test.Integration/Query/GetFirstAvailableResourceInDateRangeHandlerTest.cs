using Hotelio.Modules.Availability.Application.Query;
using Hotelio.Modules.Availability.Application.Query.Handler;
using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Modules.Availability.Infrastructure.ReadModel;
using Hotelio.Modules.Availability.Infrastructure.Repository;
using Hotelio.Shared.Event;
using Hotelio.Shared.Tests;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Hotelio.Modules.Availability.Test.Integration.Query;

public class GetFirstAvailableResourceInDateRangeHandlerTest: IDisposable
{
    private readonly ResourceDbContext _dbContext;
    private readonly EfResourceRepository _repository;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly GetFirstAvailableResourceInDateRangeHandler _queryHandler;
    private readonly SqlServerResourceStorage _storage;

    public GetFirstAvailableResourceInDateRangeHandlerTest()
    {
        var connectionString = ConfigHelper.GetSqlServerConfig().ConnectionString;
        
        var optionBuilder = new DbContextOptionsBuilder<ResourceDbContext>()
            .UseSqlServer(connectionString);
        _dbContext = new ResourceDbContext(optionBuilder.Options);
        _eventBusMock = new Mock<IEventBus>();
        _repository = new EfResourceRepository(_dbContext, _eventBusMock.Object);
        _storage = new SqlServerResourceStorage(connectionString);
        _queryHandler = new GetFirstAvailableResourceInDateRangeHandler(_storage);
    }
    
    [Fact]
    public async void TestGetFirstAvailableResourceInMiddleOfDates()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var ids = await _prepareResources();
        
        //Expected
        var resourceExpected = new Application.ReadModel.Resource(
            ids[0],
            "group-1",
            1,
            true
        );
        
        //When
        var query = new GetFirstAvailableResourceInDateRange(
            "group-1", 
            1, 
            new DateTime(2024, 1, 5), 
            new DateTime(2024, 1, 10));

        var result = await _queryHandler.Handle(query, CancellationToken.None);
        
        Assert.Equal(resourceExpected, result);
    }
    
    [Fact]
    public async void TestGetFirstAvailableResourceBetweenDates()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var ids = await _prepareResources();
        
        var resource2Id = Guid.NewGuid();
        var resource2 = Resource.Create(resource2Id, "group-1", 1, true);
        resource2.Book("owner-1", new DateTime(2024, 1, 1), new DateTime(2024, 1, 30));
        await _repository.AddAsync(resource2);
        
        //Expected
        var resourceExpected = new Application.ReadModel.Resource(
            ids[0],
            "group-1",
            1,
            true
        );
        
        //When
        var query = new GetFirstAvailableResourceInDateRange(
            "group-1", 
            1, 
            new DateTime(2024, 1, 6), 
            new DateTime(2024, 1, 9));

        var result = await _queryHandler.Handle(query, CancellationToken.None);
        
        Assert.Equal(resourceExpected, result);
    }
    
    [Fact]
    public async void TestGetFirstAvailableResourceAtStartDateSameAsEndDateExist()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var ids = await _prepareResources();
        
        var resource2Id = Guid.NewGuid();
        var resource2 = Resource.Create(resource2Id, "group-1", 1, true);
        resource2.Book("owner-1", new DateTime(2024, 1, 1), new DateTime(2024, 1, 30));
        await _repository.AddAsync(resource2);
        
        //Expected
        var resourceExpected = new Application.ReadModel.Resource(
            ids[1],
            "group-1",
            1,
            true
        );
        
        //When
        var query = new GetFirstAvailableResourceInDateRange(
            "group-1", 
            1, 
            new DateTime(2024, 1, 30), 
            new DateTime(2024, 2, 10));

        var result = await _queryHandler.Handle(query, CancellationToken.None);
        
        Assert.Equal(resourceExpected, result);
    }
    
    [Fact]
    public async void TestGetFirstAvailableResourceAtEndDateSameAsStartDateExist()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var ids = await _prepareResources();
        
        var resource2Id = Guid.NewGuid();
        var resource2 = Resource.Create(resource2Id, "group-1", 1, true);
        resource2.Book("owner-1", new DateTime(2024, 1, 1), new DateTime(2024, 1, 30));
        await _repository.AddAsync(resource2);
        
        //Expected
        var resourceExpected = new Application.ReadModel.Resource(
            ids[0],
            "group-1",
            1,
            true
        );
        
        //When
        var query = new GetFirstAvailableResourceInDateRange(
            "group-1", 
            1, 
            new DateTime(2023, 12, 15), 
            new DateTime(2024, 1, 1));

        var result = await _queryHandler.Handle(query, CancellationToken.None);
        
        Assert.Equal(resourceExpected, result);
    }
    
    [Fact]
    public async void TestGetFirstAvailableResourceForOneDay()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var ids = await _prepareResources();
        
        //Expected
        var resourceExpected = new Application.ReadModel.Resource(
            ids[0],
            "group-1",
            1,
            true
        );
        
        //When
        var query = new GetFirstAvailableResourceInDateRange(
            "group-1", 
            1, 
            new DateTime(2024, 1, 5), 
            new DateTime(2024, 1, 6));

        var result = await _queryHandler.Handle(query, CancellationToken.None);
        
        Assert.Equal(resourceExpected, result);
    }
    
    [Fact]
    public async void TestShouldReturnNullWhenTryToFindAvailableResourceWithDateEqualToStartDate()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var ids = await _prepareResources();
        
        //When
        var query = new GetFirstAvailableResourceInDateRange(
            "group-1", 
            1, 
            new DateTime(2024, 1, 1), 
            new DateTime(2024, 1, 2));

        var result = await _queryHandler.Handle(query, CancellationToken.None);
        
        Assert.Null(result);
    }
    
    [Fact]
    public async void TestShouldReturnNullWhenTryToFindAvailableResourceWithDateEqualToEndDate()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var ids = await _prepareResources();
        
        //When
        var query = new GetFirstAvailableResourceInDateRange(
            "group-1", 
            1, 
            new DateTime(2024, 1, 3), 
            new DateTime(2024, 1, 5));

        var result = await _queryHandler.Handle(query, CancellationToken.None);
        
        Assert.Null(result);
    }
    
    [Fact]
    public async void TestShouldReturnNullWhenTryToFindAvailableResourceBeetwenDates()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var ids = await _prepareResources();
        
        //When
        var query = new GetFirstAvailableResourceInDateRange(
            "group-1", 
            1, 
            new DateTime(2024, 1, 2), 
            new DateTime(2024, 1, 4));

        var result = await _queryHandler.Handle(query, CancellationToken.None);
        
        Assert.Null(result);
    }
    
    [Fact]
    public async void TestShouldReturnNullWhenTryToFindAvailableResourceOverDates()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var ids = await _prepareResources();
        
        //When
        var query = new GetFirstAvailableResourceInDateRange(
            "group-1", 
            1, 
            new DateTime(2023, 12, 31), 
            new DateTime(2024, 1, 6));

        var result = await _queryHandler.Handle(query, CancellationToken.None);
        
        Assert.Null(result);
    }
    
    [Fact]
    public async void TestShouldReturnNullWhenTryToFindAvailableResourceOverDatesOnEndDate()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var ids = await _prepareResources();
        
        //When
        var query = new GetFirstAvailableResourceInDateRange(
            "group-1", 
            1, 
            new DateTime(2024, 1, 3), 
            new DateTime(2024, 1, 9));

        var result = await _queryHandler.Handle(query, CancellationToken.None);
        
        Assert.Null(result);
    }
    
    [Fact]
    public async void TestShouldReturnNullWhenTryToFindAvailableResourceOverDatesOnStartDate()
    {
        _dbContext.Database.EnsureCreated();
        
        //Given
        var ids = await _prepareResources();
        
        //When
        var query = new GetFirstAvailableResourceInDateRange(
            "group-1", 
            1, 
            new DateTime(2023, 12, 30), 
            new DateTime(2024, 1, 2));

        var result = await _queryHandler.Handle(query, CancellationToken.None);
        
        Assert.Null(result);
    }
    
    private async Task<string[]> _prepareResources()
    {
        var resourceId = Guid.NewGuid();
        var resource = Resource.Create(resourceId, "group-1", 1, true);
        resource.Book("owner-1", new DateTime(2024, 1, 1), new DateTime(2024, 1, 5));
        resource.Book("owner-2", new DateTime(2024, 1, 10), new DateTime(2024, 1, 14));
        await _repository.AddAsync(resource);
        
        var resource2Id = Guid.NewGuid();
        var resource2 = Resource.Create(resource2Id, "group-1", 1, true);
        resource2.Book("owner-1", new DateTime(2024, 1, 1), new DateTime(2024, 1, 30));
        await _repository.AddAsync(resource2);
        
        string[] ids = {resourceId.ToString(), resource2Id.ToString()};

        return ids;
    }
    
    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}