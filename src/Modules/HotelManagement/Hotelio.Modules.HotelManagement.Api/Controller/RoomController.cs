using Hotelio.Modules.HotelManagement.Api.Controller.Request;
using Hotelio.Modules.HotelManagement.Core.Service;
using Hotelio.Modules.HotelManagement.Core.Service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Hotelio.Modules.HotelManagement.Api.Controller;

[ApiController]
[Route("api/hotel/room")]
internal class RoomController: ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService) => _roomService = roomService;

    [HttpPost]
    public async Task<IActionResult> Add(RoomRequest request)
    {
        var id = await _roomService.AddAsync(
            new RoomDto(0, request.Number, request.MaxGuests, request.Type, request.HotelId));
        
        return Ok(new { Id = id });
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, RoomRequest request)
    {
        await _roomService.UpdateAsync(new RoomDto(id, request.Number, request.MaxGuests, request.Type, request.HotelId));
        
        return NoContent();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RoomDto>> Get(int id)
    {
        var room = await _roomService.GetAsync(id);

        if (room is null) {
            return NotFound();
        }

        return Ok(room);
    }
}