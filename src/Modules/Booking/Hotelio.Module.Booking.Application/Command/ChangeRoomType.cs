using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Domain.Model
{
    internal record ChangeRoomType(string ReservationId, int RoomType) : ICommand;
}
