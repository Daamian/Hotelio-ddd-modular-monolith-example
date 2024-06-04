using System.Net;
using Hotelio.Bootstrapper;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Hotelio.Modules.Booking.Test.EndToEnd.Reservation;

public class ReservationProcessTest
{
    
    [Fact]
    public async Task GetApi()
    {
        await using var application = new WebApplicationFactory<Startup>();
        using var client = application.CreateClient();

        var response = await client.GetAsync("/api/test");
        Assert.True(response.StatusCode.Equals(HttpStatusCode.OK));
    }

    [Fact]
    public async Task CreateReservation()
    {
        await using var application = new WebApplicationFactory<Startup>();
        using var client = application.CreateClient();
        
        //TODO Create hotel on api - first implement event on cross contract
        var hotelId = await _createHotel(client);
        
        Assert.True(true);

        //Step 1: Create reservation
        /*var reservation = new
        {
            Id = Guid.NewGuid(),
            HotelId = "Hotel-1",
            OwnerId = "Owner-1",
            Amenities = new[] { "amenity-1" },
            RoomType = 1,
            PriceToPay = 100.0,
            PaymentType = 1,
            NumberOfGuests = 2,
            StartDate = "2023-01-01",
            EndDate = "2023-01-19"
        };

        var content = new StringContent(JsonSerializer.Serialize(reservation), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/reservation", content);
        var r = response.Content.ToString();
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);*/
        
        //Step 2: Get created reservation and check response
        /*var responseGet = await client.GetAsync($"/api/reservation/{reservation.Id.ToString()}");
        var expected = new
        {
            id = reservation.Id,
            hotel = new { id = "Hotel-1", name = "Hotel name" },
            owner = new { id = "Owner-1", name = "Damian", surname = "Kusek" },
            roomType = new { id = 1, name = "Superior" },
            numberOfGuests = 2,
            status = "Confirmed",
            priceToPay = 100,
            pricePayed = 0,
            paymentType = "PostPaid",
            startDate = "2023-01-01T00:00:00Z", //TODO jak przechowywać datę w bazie mongo ????
            endDate = "2023-01-19T00:00:00Z",
            amenities = new[] { new { id = "amenity-1", name = "All inc" } },
            roomId = "ccac553d-d50d-4785-806a-7e32fdea3c23"
        };
        
        Assert.True(responseGet.StatusCode.Equals(HttpStatusCode.OK));
        Assert.Equal(JsonSerializer.Serialize(expected), await responseGet.Content.ReadAsStringAsync());*/
    }

    private async Task<int> _createHotel(HttpClient client)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { Name = "Test hotel"}), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/hotel", content);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
        int id = jsonResponse.id;

        await _createRoom(client, id);
        await _createRoom(client, id);
        await _createRoom(client, id);
        
        return id;
    }

    private async Task<int> _createRoom(HttpClient client, int hotelId)
    {
        var payload = new 
        {
            Number = 100,
            MaxGuests = 2, 
            Type = 1,
            HotelId = hotelId
        };
        
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/hotel/room", content);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
        int id = jsonResponse.id;

        return id;
    }
}