using System;
using System.Threading.Tasks;
using Hotelio.Modules.Booking.Application.ReadModel;
using Hotelio.Modules.Booking.Infrastructure.Storage;
using Hotelio.Modules.Booking.Application.ReadModel.VO;
using System.Collections.Generic;
using System.Linq;

namespace Hotelio.Modules.Booking.Infrastructure.ReadModel;

internal sealed class InMemoryReadModelStorage : IReadModelStorage
{
    public async Task<Reservation> FindAsync(Guid reservationId)
    {
        var reservation = InMemoryStorage.reservations.Find(
            r => (string) r.Snapshot()["Id"] == reservationId.ToString()
        );

        var snapshot = reservation.Snapshot();
        var dateRange = (Domain.Model.DateRange) snapshot["DateRange"];
        List<Domain.Model.Amenity> amenities = (List<Domain.Model.Amenity>)snapshot["Amenities"];

        return new Reservation(
            new Guid((string)snapshot["Id"]),
            new Hotel((string)snapshot["HotelId"], "Hotel name"),
            new Owner((string) snapshot["OwnerId"], "Damian", "Kusek"),
            new RoomType((int) snapshot["RoomType"], "Room type name"),
            (int)snapshot["NumberOfGuests"],
            snapshot["Status"].ToString(),
            (double)snapshot["PriceToPay"],
            (double)snapshot["PricePayed"],
            (string)snapshot["PaymentType"].ToString(),
            dateRange.StartDate,
            dateRange.EndDate,
            amenities.Select(a => new Amenity(a.Id, "Amenity name")).ToList()
        );
    }
}