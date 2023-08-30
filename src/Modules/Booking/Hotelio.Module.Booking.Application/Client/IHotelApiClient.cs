using System;
using Hotelio.Module.Booking.Application.Client.DTO;
namespace Hotelio.Module.Booking.Application.Client;

internal interface IHotelApiClient
{
    Task<Hotel> GetAsync(string id);
}


