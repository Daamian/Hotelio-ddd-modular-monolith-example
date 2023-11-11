using Hotelio.Modules.Availability.Domain.Exception;

namespace Hotelio.Modules.Availability.Domain.Model;

internal class Resource
{
    private Guid _id;
    private string _groupId;
    private HashSet<Book> _books = new HashSet<Book>();
    private bool _isActive;

    private Resource(Guid id, bool active = true)
    {
        this._id = id;
        this._isActive = active;
    }
    
    public static Resource Create(Guid id, bool active = true)
    {
        return new Resource(id, active);
    }
    
    public void Book(string ownerId, DateTime startDate, DateTime endDate)
    {
        if (IsBooked(startDate, endDate))
        {
            throw new ResourceIsBookedException("Resource is booked in given period");
        }

        if (false == this._isActive)
        {
            throw new ResourceIsNotActiveException("Resource is not active");
        }

        this._books.Add(new Book(ownerId, startDate, endDate));
    }

    public void UnBook(string ownerId, DateTime startDate, DateTime endDate)
    {
        var bookFound = this._books.SingleOrDefault(book =>
            book.StartDate == startDate && book.EndDate == endDate && book.OwnerId == ownerId);
        
        if (bookFound is null)
        {
            throw new BookNotExistsException("Book not exist in this resource");
        }

        this._books.Remove(bookFound);
    }

    private bool IsBooked(DateTime startDate, DateTime endDate)
    {
        var booksInDateRange = this._books.Where(book =>
            book.StartDate >= startDate && book.EndDate <= endDate);

        return booksInDateRange.Any();
    }
    
    public IDictionary<string, object> Snapshot()
    {
        return new Dictionary<string, object>
        {
            { "Id", this._id },
            { "IsActive", this._isActive },
            { "Books", this._books }
        };
    }
}