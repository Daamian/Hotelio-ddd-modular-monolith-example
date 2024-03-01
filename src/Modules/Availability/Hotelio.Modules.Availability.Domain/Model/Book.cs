namespace Hotelio.Modules.Availability.Domain.Model;

internal class Book
{ 
    public Guid BookId { get; private set; }
    public string OwnerId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public Book(Guid bookId, string ownerId, DateTime startDate, DateTime endDate)
    {
        BookId = bookId;
        OwnerId = ownerId;
        StartDate = startDate;
        EndDate = endDate;
    }
}