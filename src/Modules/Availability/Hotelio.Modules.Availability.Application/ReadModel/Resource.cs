namespace Hotelio.Modules.Availability.Application.ReadModel;

internal record Resource(
    string Id,
    string Group,
    int Type,
    bool IsActive
    );