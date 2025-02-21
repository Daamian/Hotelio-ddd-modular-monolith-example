using Hotelio.Modules.Pricing.Application.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hotelio.Modules.Pricing.Api.Controller;

[ApiController]
[Route("api/hotel-tariffs")]
internal class HotelTariffController : ControllerBase
{
    private readonly IMediator _mediator;

    public HotelTariffController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateHotelTariff([FromBody] CreateHotelTariff command)
    {
        var hotelTariffId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetHotelTariff), new { id = hotelTariffId }, new { hotelTariffId });
    }
    
    [HttpPost("{hotelTariffId}/rooms")]
    public async Task<IActionResult> AddRoomTariff(Guid hotelTariffId, [FromBody] AddRoomTariff command)
    {
        var request = command with { HotelTariffId = hotelTariffId };
        await _mediator.Send(request);
        return NoContent();
    }
    
    [HttpPost("{hotelTariffId}/amenities")]
    public async Task<IActionResult> AddAmenityTariff(Guid hotelTariffId, [FromBody] AddAmenityTariff command)
    {
        var request = command with { HotelTariffId = hotelTariffId };
        await _mediator.Send(request);
        return NoContent();
    }
    
    [HttpPost("{hotelTariffId}/rooms/period-prices")]
    public async Task<IActionResult> AddPeriodPrice(Guid hotelTariffId, [FromBody] AddRoomPeriodPrice command)
    {
        var request = command with { HotelTariffId = hotelTariffId };
        await _mediator.Send(request);
        return NoContent();
    }
    
    [HttpGet("{id}")]
    public IActionResult GetHotelTariff(Guid id)
    {
        // Mock response (implement actual query in the future)
        return Ok(new { Id = id });
    }
}
