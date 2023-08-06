using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}

