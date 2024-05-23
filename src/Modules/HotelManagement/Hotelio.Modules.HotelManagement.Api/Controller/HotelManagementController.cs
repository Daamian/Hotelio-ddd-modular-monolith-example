using Hotelio.Modules.HotelManagement.Api.Controller.Request;
using Hotelio.Modules.HotelManagement.Core.Service;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Hotelio.Modules.HotelManagement.Api.Controller;

[ApiController]
[Route("api/hotel")]
internal class HotelManagementController : ControllerBase
{
    private IHotelService _hotelService;

    public HotelManagementController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }
    
    [HttpPost]
    public IActionResult Create(HotelRequest request)
    {
        var id = _hotelService.Add(new HotelDto(0, request.Name));
        return NoContent();
    }
    
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, HotelRequest request)
    {
        _hotelService.Update(new HotelDto(id, request.Name));
        return NoContent();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<HotelDto>> Get(int id)
    {
        var hotel = _hotelService.Get(id);

        if (hotel is null) {
            return NotFound();
        }

        return Ok(hotel);
    }
}