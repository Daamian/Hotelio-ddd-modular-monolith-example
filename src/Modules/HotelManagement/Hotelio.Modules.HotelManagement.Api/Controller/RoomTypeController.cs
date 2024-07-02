using Hotelio.Modules.HotelManagement.Api.Controller.Request;
using Hotelio.Modules.HotelManagement.Core.Service;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Hotelio.Modules.HotelManagement.Api.Controller;

[ApiController]
[Route("api/hotel/room_type")]
internal class RoomTypeController: ControllerBase
{
    private readonly IRoomTypeService _roomTypeService;

    public RoomTypeController(IRoomTypeService roomTypeService) => _roomTypeService = roomTypeService;

    [HttpPost]
    public async Task<IActionResult> Add(RoomTypeRequest request)
    {
        var id = await _roomTypeService.AddAsync(
            new RoomTypeDto(0, request.Name, request.MaxGuests, request.Level));
        
        return Ok(new { Id = id });
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, RoomTypeRequest request)
    {
        await _roomTypeService.UpdateAsync(new RoomTypeDto(id, request.Name, request.MaxGuests, request.Level));
        
        return NoContent();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RoomTypeDto>> Get(int id)
    {
        var room = await _roomTypeService.GetAsync(id);

        if (room is null) {
            return NotFound();
        }

        return Ok(room);
    }
}