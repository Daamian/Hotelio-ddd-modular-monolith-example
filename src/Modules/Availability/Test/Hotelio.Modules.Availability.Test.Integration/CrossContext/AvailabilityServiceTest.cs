
using Hotelio.CrossContext.Contract.Availability.Exception;
using Hotelio.Modules.Availability.Api.CrossContext;
using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Domain.Repository;
using BookCommand = Hotelio.Modules.Availability.Application.Command.Book;
using CreateCommand = Hotelio.Modules.Availability.Application.Command.Create;
using Hotelio.Shared.Commands;

using Moq;

namespace Hotelio.Modules.Availability.Test.Integration.CrossContext;

[Collection("Database collection")]
public class AvailabilityServiceTest
{
    private readonly Mock<IResourceRepository> _repositoryMock = new();
    private readonly Mock<ICommandBus> _commandBusMock = new();
    private readonly AvailabilityService _availabilityService;
    
    public AvailabilityServiceTest()
    {
        _repositoryMock = new Mock<IResourceRepository>();
        _commandBusMock = new Mock<ICommandBus>();
        _availabilityService = new AvailabilityService(_commandBusMock.Object, _repositoryMock.Object);
    }
    
    [Fact]
    public async Task BookFirstAvailableAsyncWithAvailableResource()
    {
        // Given
        var resourceId = "Resource123";
        var internalId = Guid.NewGuid();
        _repositoryMock.Setup(q => q.FindByExternalIdAsync(resourceId))
            .ReturnsAsync(Resource.Create(internalId, resourceId));

        // When
        await _availabilityService.BookAsync(resourceId, "Owner123", DateTime.UtcNow, DateTime.UtcNow.AddDays(1));

        // Assert
        _commandBusMock.Verify(c => c.DispatchAsync(It.Is<BookCommand>(b => b.ResourceId == internalId && b.OwnerId == "Owner123")), Times.Once);
    }

    [Fact]
    public async Task CreateResource()
    {
        //Given
        var resourceId = "Resource123";
        
        //When
        await _availabilityService.CreateResource(resourceId);
        
        //Then
        _commandBusMock.Verify(c => c.DispatchAsync(It.Is<CreateCommand>(c => c.ExternalId == resourceId)), Times.Once);

    }

    [Fact]
    public async Task BookFirstAvailableAsyncWithoutAvailableResource()
    {
        // Given
        var resourceId = "Resource123";
        var internalId = Guid.NewGuid();
        _repositoryMock.Setup(q => q.FindByExternalIdAsync(resourceId))
            .ReturnsAsync((Resource) null);

        // Act & Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(() =>
            _availabilityService.BookAsync(resourceId, "Owner123", DateTime.UtcNow, DateTime.UtcNow.AddDays(1)));
    }
}