using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Availability.Application.Command;

public record UnBook(string ResourceId, string BookId): ICommand;