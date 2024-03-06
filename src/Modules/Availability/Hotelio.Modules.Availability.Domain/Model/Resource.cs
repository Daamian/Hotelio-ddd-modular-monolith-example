using Hotelio.Modules.Availability.Domain.Event;
using Hotelio.Modules.Availability.Domain.Exception;
using Hotelio.Shared.Domain;
using MassTransit.Futures.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotelio.Modules.Availability.Domain.Model;

internal class Resource: Aggregate
{
    public Guid Id { get; private set; }
    public string GroupId { get; private set; }
    public int Type { get; private set; }
    public bool IsActive { get; private set; }
    
    private List<Book> _books = new List<Book>();
    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    private Resource(Guid id, string groupId, int type, bool active = true)
    {
        this.Id = id;
        this.GroupId = groupId;
        this.Type = type;
        this.IsActive = active;
    }

    protected Resource()
    {
        
    }
    
    public static Resource Create(Guid id, string groupId, int type, bool active = true)
    {
        return new Resource(id, groupId, type, active);
    }

    public void ChangeGroup(string newGroupId)
    {
        this.GroupId = newGroupId;
    }
    
    public void Book(string ownerId, DateTime startDate, DateTime endDate)
    {
        if (IsBooked(startDate, endDate))
        {
            throw new ResourceIsBookedException("Resource is booked in given period");
        }

        if (false == this.IsActive)
        {
            throw new ResourceIsNotActiveException("Resource is not active");
        }

        var book = new Book(ownerId, startDate, endDate);
        this._books.Add(book);
        this.Events.Add(new ResourceBooked(this.Id.ToString(), book.OwnerId, book.StartDate, book.EndDate));
    }

    public void UnBook(Guid bookId)
    {
        var bookFound = this._books.SingleOrDefault(book => book.Id.Equals(bookId));
        
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
            { "Id", this.Id },
            { "IsActive", this.IsActive },
            { "GroupId", this.GroupId },
            { "Type", this.Type },
            { "Books", this._books }
        };
    }
}