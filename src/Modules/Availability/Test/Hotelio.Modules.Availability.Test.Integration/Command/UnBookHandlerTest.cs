using Hotelio.Modules.Availability.Application.Command;
using Hotelio.Modules.Availability.Application.Command.Handlers;
using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Modules.Availability.Infrastructure.Repository;
using Hotelio.Shared.Event;
using Hotelio.Shared.Tests;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Hotelio.Modules.Availability.Test.Integration.Command;

[Collection("Database collection")]
public class UnBookHandlerTest
{
    private readonly BookHandler _bookHandler;
    private readonly UnBookHandler _unBookHandler;
    private readonly ResourceDbContext _dbContext;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly EfResourceRepository _repository;
    
    public UnBookHandlerTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<ResourceDbContext>()
            .UseSqlServer(ConfigHelper.GetSqlServerConfig().ConnectionString);
        _dbContext = new ResourceDbContext(optionBuilder.Options);
        _eventBusMock = new Mock<IEventBus>();
        _repository = new EfResourceRepository(_dbContext, _eventBusMock.Object);
        _unBookHandler = new UnBookHandler(_repository);
        _bookHandler = new BookHandler(_repository);
    }

    [Fact]
    public async void UnBookResourceWithOneTest()
    {
        //Given
        var resourceId = Guid.NewGuid();
        var resource = Resource.Create(resourceId, Guid.NewGuid().ToString(),"group-1", 1, true);
        resource.Book(
            "owner-id", 
            new DateTime(2024, 2, 1), 
            new DateTime(2024, 2, 5));

        await _repository.AddAsync(resource);
        var bookId = resource.Books.First().Id;
        
        //When
        var command = new UnBook(resourceId.ToString(), bookId.ToString());
        await _unBookHandler.Handle(command, CancellationToken.None);
        
        //Then
        var resourceFound = await _repository.FindAsync(resourceId);
        
        Assert.Empty(resourceFound.Books);
    }

    [Fact]
    public async void UnBookResourceWithManyTest()
    {
        var resourceId = Guid.NewGuid();
        var resource = Resource.Create(resourceId, Guid.NewGuid().ToString(),"group-1", 1, true);
        resource.Book("owner-1", new DateTime(2024, 1, 1), new DateTime(2024, 1, 14));
        resource.Book("owner-2", new DateTime(2024, 1, 14), new DateTime(2024, 1, 30));
        resource.Book("owner-3", new DateTime(2024, 2, 5), new DateTime(2024, 2, 15));
        await _repository.AddAsync(resource);

        var books = resource.Books;
        var bookId = books.FirstOrDefault(r => r.OwnerId == "owner-2")!.Id;
        
        //When
        var command = new UnBook(resourceId.ToString(), bookId.ToString());
        await _unBookHandler.Handle(command, CancellationToken.None);
        
        //Then
        var resourceFound = await _repository.FindAsync(resourceId);
        
        //Then
        Assert.Collection(resourceFound.Books, book => {
            Assert.Equal("owner-1", book.OwnerId);
            Assert.Equal(new DateTime(2024, 1, 1), book.StartDate);
            Assert.Equal(new DateTime(2024, 1, 14), book.EndDate);
        }, book =>
        {
            Assert.Equal("owner-3", book.OwnerId);
            Assert.Equal(new DateTime(2024, 2, 5), book.StartDate);
            Assert.Equal(new DateTime(2024, 2, 15), book.EndDate);
        });
    }
}