namespace Hotelio.Modules.Booking.Domain.Model;

internal class DateRange
{
    public readonly DateTime StartDate;
    public readonly DateTime EndDate;

    public DateRange(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public bool isGratherThan(DateRange dateRange)
    {
        if (this.StartDate <= dateRange.StartDate && this.EndDate >= dateRange.EndDate) { return true; }

        return false;
    }
}

