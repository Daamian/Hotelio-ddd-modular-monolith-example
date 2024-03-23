using Hotelio.Modules.Availability.Application.ReadModel;
using Hotelio.Shared.Queries;

namespace Hotelio.Modules.Availability.Application.Query;

internal record GetFirstAvailableResourceInDateRange(
    string Group, 
    int Type,
    DateTime StarDate, 
    DateTime EndDate
) : IQuery<Resource?>;