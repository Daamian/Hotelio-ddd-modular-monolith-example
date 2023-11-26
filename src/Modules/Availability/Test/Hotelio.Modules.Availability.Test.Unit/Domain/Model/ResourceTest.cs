namespace Hotelio.Modules.Availability.Test.Unit.Domain.Model;

using Hotelio.Modules.Availability.Domain.Model;
using Hotelio.Modules.Availability.Domain.Exception;

public class ResourceTest
{
    [Fact]
    public void TestShouldCreateResource()
    {
        // Arrange
        Guid resourceId = Guid.NewGuid();

        // Act
        var resource = Resource.Create(resourceId, "Hotel-1", 1);
        var snapshot = resource.Snapshot();

        // Assert
        Assert.NotNull(resource);
        Assert.Equal(resourceId, snapshot["Id"]);
        Assert.True((bool) snapshot["IsActive"]);
        Assert.Empty((HashSet<Book>) snapshot["Books"]);
    }

    [Fact]
    public void TestShouldBookResourceInDateRange()
    {
        // Arrange
        Resource resource = Resource.Create(Guid.NewGuid(), "Hotel-1", 1);
        string ownerId = "User1";
        DateTime startDate = DateTime.Now;
        DateTime endDate = startDate.AddHours(2);

        // Act
        resource.Book(ownerId, startDate, endDate);

        // Assert
        var snapshot = resource.Snapshot();
        Assert.Single((HashSet<Book>)snapshot["Books"]);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryToOverlapBookResource()
    {
        // Arrange
        Resource resource = Resource.Create(Guid.NewGuid(),"Hotel-1", 1);
        string ownerId = "User1";
        DateTime startDate = DateTime.Now;
        DateTime endDate = startDate.AddHours(2);

        // Act
        resource.Book(ownerId, startDate, endDate);

        // Assert
        Assert.Throws<ResourceIsBookedException>(() => resource.Book(ownerId, startDate, endDate));
    }

    [Fact]
    public void TestShouldUnBookResource()
    {
        // Arrange
        Resource resource = Resource.Create(Guid.NewGuid(),"Hotel-1", 1);
        string ownerId = "User1";
        DateTime startDate = DateTime.Now;
        DateTime endDate = startDate.AddHours(2);

        resource.Book(ownerId, startDate, endDate);

        // Act
        resource.UnBook(ownerId, startDate, endDate);

        // Assert
        var snapshot = resource.Snapshot();
        Assert.Empty((HashSet<Book>)snapshot["Books"]);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryUnBookNonExistBook()
    {
        // Arrange
        Resource resource = Resource.Create(Guid.NewGuid(),"Hotel-1", 1);
        string ownerId = "User1";
        DateTime startDate = DateTime.Now;
        DateTime endDate = startDate.AddHours(2);

        // Act & Assert
        Assert.Throws<BookNotExistsException>(() => resource.UnBook(ownerId, startDate, endDate));
    }
}