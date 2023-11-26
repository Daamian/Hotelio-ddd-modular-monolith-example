using Hotelio.Modules.Availability.Domain.Event;
using Hotelio.Modules.Availability.Domain.Exception;
using Hotelio.Shared.Domain;

namespace Hotelio.Modules.Availability.Domain.Model;

internal class Resource: Aggregate
{
    private Guid _id;
    private string _groupId;
    private int _type;
    private HashSet<Book> _books = new HashSet<Book>();
    private bool _isActive;

    private Resource(Guid id, string groupId, int type, bool active = true)
    {
        this._id = id;
        this._groupId = groupId;
        this._type = type;
        this._isActive = active;
    }
    
    public static Resource Create(Guid id, string groupId, int type, bool active = true)
    {
        return new Resource(id, groupId, type, active);
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

        var book = new Book(new Guid(), ownerId, startDate, endDate);
        this._books.Add(book);
        this.Events.Add(new ResourceBooked(this._id.ToString(), book.BookId.ToString(), book.OwnerId, book.StartDate, book.EndDate));
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
            { "GroupId", this._groupId },
            { "Type", this._type },
            { "Books", this._books }
        };
    }
}