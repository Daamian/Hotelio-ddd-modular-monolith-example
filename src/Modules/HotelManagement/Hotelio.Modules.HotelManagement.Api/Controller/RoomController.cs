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
    public IActionResult Add(RoomRequest request)
    {
        var id = _roomService.Add(
            new RoomDto(0, request.Number, request.MaxGuests, request.Type, request.HotelId));
        
        return Ok(new { Id = id });
    }
    
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, RoomRequest request)
    {
        _roomService.Update(new RoomDto(id, request.Number, request.MaxGuests, request.Type, request.HotelId));
        
        return NoContent();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RoomDto>> Get(int id)
    {
        var room = _roomService.Get(id);

        if (room is null) {
            return NotFound();
        }

        return Ok(room);
    }
}