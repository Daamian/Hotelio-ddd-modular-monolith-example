using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command;
internal record ChangeRoomType(string ReservationId, int RoomType) : ICommand;

