using System;
using Hotelio.Modules.Booking.Domain.Model;
using System.Collections.Generic;

namespace Hotelio.Modules.Booking.Infrastructure.Storage;

internal static class InMemoryStorage
{
    public static List<Reservation> reservations { set; get; }
}


