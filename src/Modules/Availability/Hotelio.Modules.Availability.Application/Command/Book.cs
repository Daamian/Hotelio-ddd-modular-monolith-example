
using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Availability.Application.Command;

internal record Book(string ResourceId, string OwnerId, DateTime StarDate, DateTime EndDate): ICommand;