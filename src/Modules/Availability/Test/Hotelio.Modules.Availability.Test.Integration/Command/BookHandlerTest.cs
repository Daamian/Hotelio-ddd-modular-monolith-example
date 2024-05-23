using System.Net;
using Hotelio.Modules.Availability.Domain.Exception;
using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Modules.Availability.Infrastructure.Repository;
using Hotelio.Shared.Event;
using Hotelio.Shared.Tests;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using Book = Hotelio.Modules.Availability.Application.Command.Book;

namespace Hotelio.Modules.Availability.Test.Integration.Command;

using Hotelio.Modules.Availability.Application.Command.Handlers;

[Collection("Database collection")]
public class BookHandlerTest
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
        //Given
        var resourceId = Guid.NewGuid();
        var resource = Resource.Create(resourceId, "group-1", 1, true);
        resource.Book("owner-1", new DateTime(2024, 1, 1), new DateTime(2024, 1, 14));
        resource.Book("owner-2", new DateTime(2024, 1, 14), new DateTime(2024, 1, 30));
        resource.Book("owner-3", new DateTime(2024, 2, 5), new DateTime(2024, 2, 15));
        await _repository.AddAsync(resource);
        
        //When
        var command = new Book(resourceId.ToString(), "owner-4", new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
        await _bookHandler.Handle(command, CancellationToken.None);
        
        var resourceFound = await _repository.FindAsync(resourceId);
        
        //Then
        Assert.Collection(resourceFound.Books, book => {
            Assert.Equal("owner-1", book.OwnerId);
            Assert.Equal(new DateTime(2024, 1, 1), book.StartDate);
            Assert.Equal(new DateTime(2024, 1, 14), book.EndDate);
        }, book =>
        {
            Assert.Equal("owner-2", book.OwnerId);
            Assert.Equal(new DateTime(2024, 1, 14), book.StartDate);
            Assert.Equal(new DateTime(2024, 1, 30), book.EndDate);
        }, book =>
        {
            Assert.Equal("owner-3", book.OwnerId);
            Assert.Equal(new DateTime(2024, 2, 5), book.StartDate);
            Assert.Equal(new DateTime(2024, 2, 15), book.EndDate);
        }, book =>
        {
            Assert.Equal("owner-4", book.OwnerId);
            Assert.Equal(new DateTime(2024, 2, 1), book.StartDate);
            Assert.Equal(new DateTime(2024, 2, 5), book.EndDate);
        });
    }
}