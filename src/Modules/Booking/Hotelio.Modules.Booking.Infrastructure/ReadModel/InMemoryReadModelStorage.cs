﻿using System;
using System.Threading.Tasks;
using Hotelio.Module.Booking.Application.ReadModel;
using Hotelio.Modules.Booking.Infrastructure.Storage;
using Hotelio.Module.Booking.Application.ReadModel.VO;
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
            new Hotel(new Guid((string)snapshot["HotelId"]), "Hotel name"),
            new RoomType((int) snapshot["RoomType"], "Room type name"),
            (int)snapshot["NumberOfGuests"],
            (string)snapshot["Status"],
            (double)snapshot["PriceToPay"],
            (double)snapshot["PricePayed"],
            (string)snapshot["PaymentType"],
            dateRange.StartDate,
            dateRange.EndDate,
            amenities.Select(a => new Amenity(new Guid(a.Id), "Amenity name")).ToList()
        );
    }
}