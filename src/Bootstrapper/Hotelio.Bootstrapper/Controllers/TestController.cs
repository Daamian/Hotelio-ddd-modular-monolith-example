using System;
using Microsoft.AspNetCore.Mvc;

namespace Hotelio.Bootstrapper.Controllers;

[ApiController]
[Route("api/test")]
public class ReservationController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok("Hello test docker");
    }
}


