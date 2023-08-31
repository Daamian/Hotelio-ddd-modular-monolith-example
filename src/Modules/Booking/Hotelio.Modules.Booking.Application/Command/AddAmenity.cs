using Hotelio.Shared.Commands;

namespace Hotelio.Modules.Booking.Application.Command;

internal record AddAmenity(string ReservationId, string Amenity) : ICommand;

