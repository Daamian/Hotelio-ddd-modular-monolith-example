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
        Resource resource = Resource.Create(resourceId, Guid.NewGuid().ToString());
        var snapshot = resource.Snapshot();

        // Assert
        Assert.NotNull(resource);
        Assert.Equal(resourceId, snapshot["Id"]);
        Assert.True((bool) snapshot["IsActive"]);
        Assert.Empty(resource.Books);
    }

    [Fact]
    public void TestShouldBookResourceInDateRange()
    {
        // Arrange
        Resource resource = Resource.Create(Guid.NewGuid(), Guid.NewGuid().ToString());
        string ownerId = "User1";
        DateTime startDate = DateTime.Now;
        DateTime endDate = startDate.AddHours(2);

        // Act
        resource.Book(ownerId, startDate, endDate);

        // Assert
        var snapshot = resource.Snapshot();
        Assert.Single(resource.Books);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryToOverlapBookResource()
    {
        // Arrange
        Resource resource = Resource.Create(Guid.NewGuid(), Guid.NewGuid().ToString());
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
        Resource resource = Resource.Create(Guid.NewGuid(), Guid.NewGuid().ToString());
        string ownerId = "User1";
        DateTime startDate = DateTime.Now;
        DateTime endDate = startDate.AddHours(2);

        resource.Book(ownerId, startDate, endDate);
        var bookId = resource.Books.First().Id;

        // Act
        resource.UnBook(bookId);

        // Assert
        var snapshot = resource.Snapshot();
        Assert.Empty(resource.Books);
    }

    [Fact]
    public void TestShouldThrowDomainExceptionWhenITryUnBookNonExistBook()
    {
        // Arrange
        Resource resource = Resource.Create(Guid.NewGuid(), Guid.NewGuid().ToString());
        string ownerId = "User1";
        DateTime startDate = DateTime.Now;
        DateTime endDate = startDate.AddHours(2);

        var bookId = new Guid("74c780d4-294b-4bde-ae6e-04f560eb1775");

        // Act & Assert
        Assert.Throws<BookNotExistsException>(() => resource.UnBook(bookId));
    }
}