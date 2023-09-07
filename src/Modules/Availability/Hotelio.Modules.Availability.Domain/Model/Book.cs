namespace Hotelio.Modules.Availability.Domain.Model;

internal class Book
{
    public string OwnerId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public Book(string ownerId, DateTime startDate, DateTime endDate)
    {
        OwnerId = ownerId;
        StartDate = startDate;
        EndDate = endDate;
    }
}