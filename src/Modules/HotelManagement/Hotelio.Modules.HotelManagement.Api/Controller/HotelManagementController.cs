using Hotelio.Modules.HotelManagement.Api.Controller.Request;
using Hotelio.Modules.HotelManagement.Core.Service;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Hotelio.Modules.HotelManagement.Api.Controller;

[ApiController]
[Route("api/hotel")]
internal class HotelManagementController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelManagementController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(HotelRequest request)
    {
        var id = await _hotelService.AddAsync(new HotelDto(0, request.Name));
        return Ok(new { Id = id });
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, HotelRequest request)
    {
        await _hotelService.UpdateAsync(new HotelDto(id, request.Name));
        return NoContent();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<HotelDto>> Get(int id)
    {
        var hotel = await _hotelService.GetAsync(id);

        if (hotel is null) {
            return NotFound();
        }

        return Ok(hotel);
    }
}