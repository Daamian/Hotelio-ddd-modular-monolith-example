using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Domain.Model
{
    internal record AddAmenity(string ReservationId, string Amenity) : ICommand;
}
