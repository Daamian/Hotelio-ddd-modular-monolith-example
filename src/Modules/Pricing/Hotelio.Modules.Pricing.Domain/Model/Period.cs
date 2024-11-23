namespace Hotelio.Modules.Pricing.Domain.Model;

internal record Period(DateTime StartDate, DateTime EndDate)
{
    public bool Overlaps(Period other)
    {
        return StartDate < other.EndDate && EndDate > other.StartDate;
    }
};