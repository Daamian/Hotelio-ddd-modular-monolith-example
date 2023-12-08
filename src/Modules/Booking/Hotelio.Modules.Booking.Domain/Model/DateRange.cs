namespace Hotelio.Modules.Booking.Domain.Model;

internal record DateRange(DateTime StartDate, DateTime EndDate)
{
    public bool IsGratherThan(DateRange dateRange)
    {
        if (this.StartDate <= dateRange.StartDate && this.EndDate >= dateRange.EndDate) { return true; }

        return false;
    }
}

