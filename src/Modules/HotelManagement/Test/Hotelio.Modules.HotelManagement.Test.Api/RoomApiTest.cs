using System.Net;
using System.Text;
using Hotelio.Bootstrapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Hotelio.Modules.HotelManagement.Test.Api;

public class RoomApiTest
{
    [Fact]
    public async Task testCreate()
    {
        await using var application = new WebApplicationFactory<Startup>();
        using var client = application.CreateClient();

        var hotelId = await _createHotel(client);

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
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<int>(id);
    }

    [Fact]
    public async Task testGet()
    {
        await using var application = new WebApplicationFactory<Startup>();
        using var client = application.CreateClient();

        var hotelId = await _createHotel(client);

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
        
        var responseGet = await client.GetAsync($"/api/hotel/room/{id}");

        var expected = new
        {
            id = id,
            number = 100,
            maxGuests = 2,
            type = 1,
            hotelId = hotelId
        };
        
        Assert.True(responseGet.StatusCode.Equals(HttpStatusCode.OK));
        Assert.Equal(JsonSerializer.Serialize(expected), await responseGet.Content.ReadAsStringAsync());
    }
    
    [Fact]
    public async Task testUpdate()
    {
        await using var application = new WebApplicationFactory<Startup>();
        using var client = application.CreateClient();

        var hotelId = await _createHotel(client);

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
        
        var payloadUpdate = new 
        {
            Number = 111,
            MaxGuests = 3, 
            Type = 2,
            HotelId = hotelId
        };
        
        var contentUpdate = new StringContent(JsonSerializer.Serialize(payloadUpdate), Encoding.UTF8, "application/json");
        var responseUpdate = await client.PutAsync($"/api/hotel/room/{id}", contentUpdate);
        
        Assert.True(responseUpdate.StatusCode.Equals(HttpStatusCode.NoContent));
    }
    
    private async Task<int> _createHotel(HttpClient client)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { Name = "Test hotel"}), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/hotel", content);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
        
        return jsonResponse.id;
    }
}