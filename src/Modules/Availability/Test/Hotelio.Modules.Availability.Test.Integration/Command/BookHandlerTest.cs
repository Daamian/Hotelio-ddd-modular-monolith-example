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

public class BookHandlerTest
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
    public void BookResourceTest()
    {
        //Given
        /*_dbContext.Database.EnsureCreated();
        var resourceId = Guid.NewGuid();
        var resource = Resource.Create(resourceId, "group-1", 1, true);
        _repository.Add(resource);*/

        var startDate = new DateTime(2024, 2, 1);
        var endDate = new DateTime(2024, 2, 5);


        //var resourceId = new Guid("42766282-cb62-46dc-82e8-6d2ff88fc5c9");
        
        //Expected
        //var resourceExpected = Resource.Create(resourceId, "group-1", 1, true);
        //resourceExpected.Book("owner-1", startDate, endDate);
        
        //When
        var resourceId = new Guid("f614d2dd-feab-465a-9caa-1d7308f0f174");
        var command = new Book(resourceId.ToString(), "owner-1", startDate, endDate);
        //await _bookHandler.Handle(command, CancellationToken.None);
        
        //var resourceFound = _repository.Find(resourceId);
        
        var resourceToUpdate = _repository.Find(resourceId);
        
        //TODO try domain exception and dispatch events ???
        resourceToUpdate.Book(command.OwnerId, command.StarDate, command.EndDate);
        //resourceToUpdate.ChangeGroup("testnewgroupid");
        _repository.Update(resourceToUpdate);
        
        //Then
        //TODO assert read model
        Assert.True(true);
        //Assert.Equal(resourceExpected.Books, resourceFound.Books);
    }

    [Fact]
    public void BookResourceV2Test()
    {
        var resourceId = new Guid("f614d2dd-feab-465a-9caa-1d7308f0f174");
        var resourceToUpdate = _repository.Find(resourceId);
        var startDate = new DateTime(2024, 2, 1);
        var endDate = new DateTime(2024, 2, 5);
        
        resourceToUpdate.Book("owner-1", startDate, endDate);
        _repository.Update(resourceToUpdate);
    }
}