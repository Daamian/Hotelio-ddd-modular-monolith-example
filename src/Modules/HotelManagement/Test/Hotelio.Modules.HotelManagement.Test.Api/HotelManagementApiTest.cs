using System.Net;
using System.Text;
using Hotelio.Bootstrapper;
using Microsoft.AspNetCore.Mvc.Testing;
using MongoDB.Bson;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Hotelio.Modules.HotelManagement.Test.Api;

public class HotelManagementApiTest
{
    [Fact]
    public async Task testCreate()
    {
        await using var application = new WebApplicationFactory<Startup>();
        using var client = application.CreateClient();
        
        var content = new StringContent(JsonSerializer.Serialize(new { Name = "Test hotel"}), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/hotel", content);
        
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
        
        var content = new StringContent(JsonSerializer.Serialize(new { Name = "Test hotel"}), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/hotel", content);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
        int id = jsonResponse.id;
        
        var responseGet = await client.GetAsync($"/api/hotel/{id}");

        var expected = new
        {
            id = id,
            name = "Test hotel"
        };
        
        Assert.True(responseGet.StatusCode.Equals(HttpStatusCode.OK));
        Assert.Equal(JsonSerializer.Serialize(expected), await responseGet.Content.ReadAsStringAsync());
    }
    
    [Fact]
    public async Task testUpdate()
    {
        await using var application = new WebApplicationFactory<Startup>();
        using var client = application.CreateClient();
        
        var content = new StringContent(JsonSerializer.Serialize(new { Name = "Test hotel"}), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/hotel", content);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
        int id = jsonResponse.id;
        
        var contentUpdate = new StringContent(JsonSerializer.Serialize(new { Name = "Test hotel"}), Encoding.UTF8, "application/json");
        var responseUpdate = await client.PutAsync($"/api/hotel/{id}", content);
        
        Assert.True(responseUpdate.StatusCode.Equals(HttpStatusCode.NoContent));
    }
}