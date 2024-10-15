using Hotelio.Modules.Availability.Domain.Event;
using Hotelio.Modules.Availability.Domain.Exception;
using Hotelio.Shared.Domain;

namespace Hotelio.Modules.Availability.Domain.Model;

internal class Resource: Aggregate
{
    public Guid Id { get; private set; }
    public string ExternalId { get; private set;  }
    public bool IsActive { get; private set; }
    
    private List<Book> _books = new();
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    private Resource(Guid id, string externalId, bool active = true)
    {
        Id = id;
        ExternalId = externalId;
        IsActive = active;
    }

    protected Resource()
    {
        
    }
    
    public static Resource Create(Guid id, string externalId, bool active = true)
    {
        return new Resource(id, externalId, active);
    }
    
    public void Book(string ownerId, DateTime startDate, DateTime endDate)
    {
        if (IsBooked(startDate, endDate))
        {
            throw new ResourceIsBookedException("Resource is booked in given period");
        }

        if (false == IsActive)
        {
            throw new ResourceIsNotActiveException("Resource is not active");
        }

        var book = new Book(ownerId, startDate, endDate);
        _books.Add(book);
        Events.Add(new ResourceBooked(
            Id.ToString(), 
            ExternalId, 
            book.OwnerId, 
            book.StartDate, 
            book.EndDate)
        );
    }

    public void UnBook(Guid bookId)
    {
        var bookFound = _books.SingleOrDefault(book => book.Id.Equals(bookId));
        
        if (bookFound is null)
        {
            throw new BookNotExistsException("Book not exist in this resource");
        }

        _books.Remove(bookFound);
    }

    private bool IsBooked(DateTime startDate, DateTime endDate)
    {
        var booksInDateRange = _books.Where(book =>
            book.StartDate >= startDate && book.EndDate <= endDate);

        return booksInDateRange.Any();
    }
}