namespace Hotelio.Modules.Availability.Application.ReadModel;

public record Book(
    string Id,
    string OwnerId,
    DateTime StarDate,
    DateTime EndDate);