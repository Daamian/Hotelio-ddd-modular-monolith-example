namespace Hotelio.Modules.Availability.Application.ReadModel;

public record Resource(
    string Id,
    string Group,
    int Type,
    bool IsActive,
    List<Book> Books
    );