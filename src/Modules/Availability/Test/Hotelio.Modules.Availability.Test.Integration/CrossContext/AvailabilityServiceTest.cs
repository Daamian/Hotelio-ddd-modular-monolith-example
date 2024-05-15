
using Hotelio.CrossContext.Contract.Availability.Exception;
using Hotelio.Modules.Availability.Api.CrossContext;
using Hotelio.Modules.Availability.Application.Query;
using Hotelio.Modules.Availability.Domain.Model;
using BookCommand = Hotelio.Modules.Availability.Application.Command.Book;
using Hotelio.Shared.Commands;
using ResourceReadModel = Hotelio.Modules.Availability.Application.ReadModel.Resource;
using Hotelio.Shared.Queries;

using Moq;

namespace Hotelio.Modules.Availability.Test.Integration.CrossContext;

public class AvailabilityServiceTest
{
    private readonly Mock<IQueryBus> _queryBusMock = new();
    private readonly Mock<ICommandBus> _commandBusMock = new();
    private readonly AvailabilityService _availabilityService;
    
    public AvailabilityServiceTest()
    {
        _queryBusMock = new Mock<IQueryBus>();
        _commandBusMock = new Mock<ICommandBus>();
        _availabilityService = new AvailabilityService(_queryBusMock.Object, _commandBusMock.Object);
    }
    
    [Fact]
    public async Task BookFirstAvailableAsyncWithAvailableResource()
    {
        // Given
        var resourceId = "Resource123";
        _queryBusMock.Setup(q => q.QueryAsync(It.IsAny<GetFirstAvailableResourceInDateRange>()))
            .ReturnsAsync(new ResourceReadModel(resourceId, "groupId", 1, true));

        // When
        await _availabilityService.BookFirstAvailableAsync("Group", 1, "Owner123", DateTime.UtcNow, DateTime.UtcNow.AddDays(1));

        // Assert
        _commandBusMock.Verify(c => c.DispatchAsync(It.Is<BookCommand>(b => b.ResourceId == resourceId && b.OwnerId == "Owner123")), Times.Once);
    }

    [Fact]
    public async Task BookFirstAvailableAsyncWithoutAvailableResource()
    {
        // Given
        _queryBusMock.Setup(q => q.QueryAsync(It.IsAny<GetFirstAvailableResourceInDateRange>()))
            .ReturnsAsync((ResourceReadModel)null);

        // Act & Assert
        await Assert.ThrowsAsync<ResourceIsNotAvailableException>(() =>
            _availabilityService.BookFirstAvailableAsync("Group", 1, "Owner123", DateTime.UtcNow, DateTime.UtcNow.AddDays(1)));
    }
}