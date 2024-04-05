using Hotelio.Modules.Availability.Domain.Exception;
using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Modules.Availability.Infrastructure.Repository;
using Hotelio.Shared.Event;
using Hotelio.Shared.SqlServer;
using Hotelio.Shared.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Book = Hotelio.Modules.Availability.Application.Command.Book;
using BookModel = Hotelio.Modules.Availability.Domain.Model.Book;

namespace Hotelio.Modules.Availability.Test.Integration.Command;

using Hotelio.Modules.Availability.Application.Command.Handlers;

public class BookHandlerTest : IDisposable
{
    private readonly BookHandler _bookHandler;
    private readonly ResourceDbContext _dbContext;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly EfResourceRepository _repository;
    
    public BookHandlerTest()
    {
        
        var optionBuilder = new DbContextOptionsBuilder<ResourceDbContext>()
            .UseSqlServer(ConfigHelper.GetSqlServerConfig().ConnectionString);
        _dbContext = new ResourceDbContext(optionBuilder.Options);
        _eventBusMock = new Mock<IEventBus>();
        _repository = new EfResourceRepository(_dbContext, _eventBusMock.Object);
        _bookHandler = new BookHandler(_repository);
    }
    
    [Fact]
    public async void OneBookResourceTest()
    {
        //Given
        _dbContext.Database.EnsureCreated();
        var resourceId = Guid.NewGuid();
        var resource = Resource.Create(resourceId, "group-1", 1, true);
        await _repository.AddAsync(resource);

        var startDate = new DateTime(2024, 2, 1);
        var endDate = new DateTime(2024, 2, 5);
        
        //Expected
        var resourceExpected = Resource.Create(resourceId, "group-1", 1, true);
        resourceExpected.Book("owner-1", startDate, endDate);
        
        //When
        var command = new Book(resourceId.ToString(), "owner-1", startDate, endDate);
        await _bookHandler.Handle(command, CancellationToken.None);
        
        var resourceFound = await _repository.FindAsync(resourceId);
        
        //Then
        Assert.Collection(resourceFound.Books, book => {
            Assert.Equal("owner-1", book.OwnerId);
            Assert.Equal(startDate, book.StartDate);
            Assert.Equal(endDate, book.EndDate);
        });
    }

    [Fact]
    public async void TryToBookBusyResource()
    {
        //Given
        _dbContext.Database.EnsureCreated();
        
        var startDate = new DateTime(2024, 2, 1);
        var endDate = new DateTime(2024, 2, 5);
        
        var resourceId = Guid.NewGuid();
        var resource = Resource.Create(resourceId, "group-1", 1, true);
        resource.Book("owner1", startDate, endDate);
        await _repository.AddAsync(resource);
        
        //When
        var command = new Book(resourceId.ToString(), "owner-1", startDate, endDate);
        
        //Then
        await Assert.ThrowsAsync<ResourceIsBookedException>(() 
            => _bookHandler.Handle(command, CancellationToken.None));

    }

    [Fact]
    public async void ManyBookResourceTest()
    {
        //TODO after return id
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}