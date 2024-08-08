using Microsoft.AspNetCore.Mvc;

namespace Hotelio.Bootstrapper.Controllers;

[ApiController]
[Route("api/test")]
public class ReservationController : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<string>> Get()
    {
        return Ok("OK");
    }
}


