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
        
        var roomTypeBasicId = await _createRoomType(client, "Basic", 2, 1);
        var roomTypeSuperiorId = await _createRoomType(client, "Superior", 2, 2);

        var amenityBBId = await _createAmenity(client, "B&B");
        var amenityHBId = await _createAmenity(client, "HB");

        var hotelId = await _createHotel(client, "Hilton", new List<int>() { amenityBBId, amenityHBId });
        await Task.Delay(250);
        var room1 = await _createRoom(client, hotelId, 100, roomTypeBasicId);
        var room2 = await _createRoom(client, hotelId, 101, roomTypeSuperiorId);
        var room3 = await _createRoom(client, hotelId, 102, roomTypeBasicId);
        await Task.Delay(250);
        await _createHotelTariff(client, hotelId, roomTypeSuperiorId.ToString(), amenityHBId.ToString());
        await Task.Delay(500);

        //Step 1: Create reservation
        var reservation = new
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId.ToString(),
            OwnerId = "Owner-1",
            Amenities = new[] { amenityHBId.ToString() },
            RoomType = roomTypeSuperiorId,
            PaymentType = 1,
            NumberOfGuests = 2,
            StartDate = "2024-01-01",
            EndDate = "2024-01-19"
        };

        var content = new StringContent(JsonSerializer.Serialize(reservation), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/reservation", content);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        
        //Step 2: Get created reservation and check response
        await Task.Delay(250);
        var responseGet = await client.GetAsync($"/api/reservation/{reservation.Id.ToString()}");
        var expected = new
        {
            id = reservation.Id,
            hotel = new { id = hotelId.ToString(), name = "Hilton" },
            owner = new { id = "Owner-1", name = "Damian", surname = "Kusek" },
            roomType = new { id = roomTypeSuperiorId, name = "Superior" },
            numberOfGuests = 2,
            status = "Confirmed",
            priceToPay = 350,
            pricePayed = 0,
            paymentType = "PostPaid",
            startDate = "2024-01-01T00:00:00Z", //TODO jak przechowywać datę w bazie mongo ????
            endDate = "2024-01-19T00:00:00Z",
            amenities = new[] { new { id = amenityHBId.ToString(), name = "HB" } },
            roomId = room2.ToString()
        };

        var responseGET = await responseGet.Content.ReadAsStringAsync();
        Assert.True(responseGet.StatusCode.Equals(HttpStatusCode.OK));
        Assert.Equal(JsonSerializer.Serialize(expected), responseGET);
    }

    private async Task<int> _createRoomType(HttpClient client, string name, int maxGuests, int level)
    {
        var payload = new 
        {
            Name = name,
            MaxGuests = maxGuests,
            Level = level
        };
        
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/hotel/room_type", content);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
        int id = jsonResponse.id;

        return id;
    }
    
    private async Task<int> _createAmenity(HttpClient client, string name)
    {
        var payload = new 
        {
            Name = name
        };
        
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/hotel/amenity", content);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
        int id = jsonResponse.id;

        return id;
    }

    private async Task<int> _createHotel(HttpClient client, string name, List<int> amenities)
    {
        var payload = new
        {
            Name = name,
            Amenities = amenities
        };
        
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/hotel", content);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
        int id = jsonResponse.id;
        
        return id;
    }

    private async Task<int> _createRoom(HttpClient client, int hotelId, int number, int roomTypeId)
    {
        var payload = new 
        {
            Number = number,
            Type = roomTypeId,
            HotelId = hotelId
        };
        
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/hotel/room", content);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
        int id = jsonResponse.id;

        return id;
    }
    
    private async Task _createHotelTariff(HttpClient client, int hotelId, string roomTypeId, string amenityId)
    {
        var payload = new
        {
            hotelId = hotelId.ToString(),
        };
        
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/hotel-tariffs", content);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
        string hotelTariffId = jsonResponse.hotelTariffId;
        
        await _createRoomTariff(client, hotelTariffId, roomTypeId);
        await _createAmenityTariff(client, hotelTariffId, amenityId);
    }

    private async Task _createRoomTariff(HttpClient client, string hotelTariffId, string roomTypeId)
    {
        var payload = new
        {
            roomTypeId = roomTypeId,
            basePriceNetAmount = 250.0
        };
        
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"/api/hotel-tariffs/{hotelTariffId}/rooms", content);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
    }
    
    private async Task _createAmenityTariff(HttpClient client, string hotelTariffId, string amenityId)
    {
        var payload = new
        {
            amenityId = amenityId,
            priceNetAmount = 100.0
        };
        
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"/api/hotel-tariffs/{hotelTariffId}/amenities", content);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
    }
}