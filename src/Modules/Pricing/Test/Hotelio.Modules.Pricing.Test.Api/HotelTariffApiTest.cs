using Hotelio.Bootstrapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Hotelio.Modules.Pricing.Test.Api;

using System.Net;
using System.Net.Http.Json;
using Xunit;

public class HotelTariffApiTests
{


    [Fact]
    public async Task CreateHotelTariff_ShouldReturnCreatedAndId()
    {
        await using var application = new WebApplicationFactory<Startup>();
        using var client = application.CreateClient();
        
        // Arrange
        var command = new
        {
            hotelId = "Hotel123",
            basePriceNetAmount = 100.0
        };

        // Act
        var response = await client.PostAsJsonAsync("/api/hotel-tariffs", command);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(jsonResponse.hotelTariffId);
    }

    [Fact]
    public async Task AddRoomTariff_ShouldReturnNoContent()
    {
        await using var application = new WebApplicationFactory<Startup>();
        using var client = application.CreateClient();
        
        // Arrange
        var createCommand = new
        {
            hotelId = "Hotel123",
            basePriceNetAmount = 100.0
        };
        var createResponse = await client.PostAsJsonAsync("/api/hotel-tariffs", createCommand);
        var responseContent = await createResponse.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
        var hotelTariffId = jsonResponse.hotelTariffId;

        var roomCommand = new
        {
            roomTypeId = "DeluxeRoom",
            basePriceNetAmount = 150.0
        };

        // Act
        var response = await client.PostAsJsonAsync($"/api/hotel-tariffs/{hotelTariffId}/rooms", roomCommand);
        var content = response.Content;

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task AddAmenityTariff_ShouldReturnNoContent()
    {
        await using var application = new WebApplicationFactory<Startup>();
        using var client = application.CreateClient();
        
        // Arrange
        var createCommand = new
        {
            hotelId = "Hotel123",
            basePriceNetAmount = 100.0
        };
        var createResponse = await client.PostAsJsonAsync("/api/hotel-tariffs", createCommand);
        var responseContent = await createResponse.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
        var hotelTariffId = jsonResponse.hotelTariffId;

        var amenityCommand = new
        {
            amenityId = "Breakfast",
            priceNetAmount = 20.0
        };

        // Act
        var response = await client.PostAsJsonAsync($"/api/hotel-tariffs/{hotelTariffId}/amenities", amenityCommand);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task AddPeriodPrice_ShouldReturnNoContent()
    {
        await using var application = new WebApplicationFactory<Startup>();
        using var client = application.CreateClient();
        
        // Arrange
        var createCommand = new
        {
            hotelId = "Hotel123",
            basePriceNetAmount = 100.0
        };
        var createResponse = await client.PostAsJsonAsync("/api/hotel-tariffs", createCommand);
        var responseContent = await createResponse.Content.ReadAsStringAsync();
        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
        var hotelTariffId = jsonResponse.hotelTariffId;

        var roomCommand = new
        {
            roomTypeId = "DeluxeRoom",
            basePriceNetAmount = 150.0
        };
        await client.PostAsJsonAsync($"/api/hotel-tariffs/{hotelTariffId}/rooms", roomCommand);

        var periodCommand = new
        {
            priceNetAmount = 120.0,
            startDate = "2024-12-01T00:00:00",
            endDate = "2024-12-10T00:00:00",
            roomTypeId = "DeluxeRoom"
        };

        // Act
        var response = await client.PostAsJsonAsync(
            $"/api/hotel-tariffs/{hotelTariffId}/rooms/period-prices",
            periodCommand
        );
        
        var responseContent2 = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}

