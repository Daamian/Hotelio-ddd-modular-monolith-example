using Hotelio.Modules.Availability.Application.Command;
using Hotelio.Modules.Availability.Application.Command.Handlers;
using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Infrastructure.DAL;
using Hotelio.Modules.Availability.Infrastructure.Repository;
using Hotelio.Shared.Event;
using Hotelio.Shared.Tests;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;

namespace Hotelio.Modules.Availability.Test.Integration.Command;

[Collection("Database collection")]
public class CreateHandlerTest
{
    private readonly CreateHandler _createHandler;
    private readonly ResourceDbContext _dbContext;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly EfResourceRepository _repository;
    
    public CreateHandlerTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<ResourceDbContext>()
            .UseSqlServer(ConfigHelper.GetSqlServerConfig().ConnectionString);
        _dbContext = new ResourceDbContext(optionBuilder.Options);
        _eventBusMock = new Mock<IEventBus>();
        _repository = new EfResourceRepository(_dbContext, _eventBusMock.Object);
        _createHandler = new CreateHandler(_repository);
    }

    [Fact]
    public async Task TestCreate()
    {
        //Given
        var id = Guid.NewGuid();
        var externalId = Guid.NewGuid().ToString();
        var command = new Create(id, externalId);
        
        //Expected
        var resourceExpected = Resource.Create(id, externalId);
        
        //When
        await _createHandler.Handle(command, CancellationToken.None);
        
        var resourceFound = await _repository.FindAsync(id);
        
        var obj1Str = JsonConvert.SerializeObject(resourceExpected);
        var obj2Str = JsonConvert.SerializeObject(resourceFound);
        //Then
        Assert.Equal(JsonConvert.SerializeObject(resourceExpected), JsonConvert.SerializeObject(resourceFound));
    }
}