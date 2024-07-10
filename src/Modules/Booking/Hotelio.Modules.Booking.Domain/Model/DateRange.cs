namespace Hotelio.Modules.Booking.Domain.Model;

internal record DateRange(DateTime StartDate, DateTime EndDate)
{
    public bool IsGratherThan(DateRange dateRange)
    {
        return StartDate <= dateRange.StartDate && EndDate >= dateRange.EndDate;
    }
}

