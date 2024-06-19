using Hotelio.Modules.HotelManagement.Api.Controller.Request;
using Hotelio.Modules.HotelManagement.Core.Service;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Hotelio.Modules.HotelManagement.Api.Controller;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/hotel/amenity")]
internal class AmenityController : ControllerBase
{
    private readonly IAmenityService _amenityService;

    public AmenityController(IAmenityService amenityService) => _amenityService = amenityService;

    [HttpPost]
    public async Task<IActionResult> Add(AmenityRequest request)
    {
        var id = await _amenityService.AddAsync(
            new AmenityDto(0, request.Name));
        
        return Ok(new { Id = id });
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AmenityRequest request)
    {
        await _amenityService.UpdateAsync(new AmenityDto(id, request.Name));
        
        return NoContent();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AmenityDto>> Get(int id)
    {
        var room = await _amenityService.GetAsync(id);

        if (room is null) {
            return NotFound();
        }

        return Ok(room);
    }
}