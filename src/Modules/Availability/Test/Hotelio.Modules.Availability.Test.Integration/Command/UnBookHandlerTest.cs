using Hotelio.Modules.Availability.Application.Command;
using Hotelio.Modules.Availability.Application.Command.Handlers;
using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Modules.Availability.Infrastructure.Repository;
using Hotelio.Shared.Event;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Hotelio.Modules.Availability.Test.Integration.Command;

public class UnBookHandlerTest : IDisposable
{
    private readonly BookHandler _bookHandler;
    private readonly UnBookHandler _unBookHandler;
    private readonly ResourceDbContext _dbContext;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly EFResourceRepository _repository;
    
    public UnBookHandlerTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<ResourceDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=tests;User=sa;Password=Your_password123;");
        _dbContext = new ResourceDbContext(optionBuilder.Options);
        _eventBusMock = new Mock<IEventBus>();
        _repository = new EFResourceRepository(_dbContext, _eventBusMock.Object);
        _unBookHandler = new UnBookHandler(_repository);
        _bookHandler = new BookHandler(_repository);
    }

    [Fact]
    public async void UnBookResourceTest()
    {
        //Given
        _dbContext.Database.EnsureCreated();
        var resourceId = Guid.NewGuid();
        var resource = Resource.Create(resourceId, "group-1", 1, true);
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

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}