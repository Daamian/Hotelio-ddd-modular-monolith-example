using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Availability.Application.Command;

internal record Create(
    Guid Id,
    string ExternalId
    ): ICommand;