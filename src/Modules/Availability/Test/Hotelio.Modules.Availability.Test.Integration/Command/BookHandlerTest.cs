using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Modules.Availability.Infrastructure.Repository;
using Hotelio.Shared.Event;
using Microsoft.EntityFrameworkCore;
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
    private readonly EFResourceRepository _repository;
    
    public BookHandlerTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<ResourceDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=tests;User=sa;Password=Your_password123;");
        _dbContext = new ResourceDbContext(optionBuilder.Options);
        _eventBusMock = new Mock<IEventBus>();
        _repository = new EFResourceRepository(_dbContext, _eventBusMock.Object);
        _bookHandler = new BookHandler(_repository);
    }
    
    [Fact]
    public async void BookResourceTest()
    {
        //Given
        _dbContext.Database.EnsureCreated();
        var resourceId = Guid.NewGuid();
        var resource = Resource.Create(resourceId, "group-1", 1, true);
        _repository.Add(resource);

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

    public async void UnBookResourceTest()
    {
        //TODO
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}