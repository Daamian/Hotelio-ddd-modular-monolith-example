using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using Hotelio.Shared.Commands;
using Hotelio.Module.Booking.Application.Command;
using Hotelio.Module.Booking.Application.ReadModel;
using Hotelio.Shared.Queries;
using Hotelio.Module.Booking.Application.Query;

namespace Hotelio.Module.Booking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
internal class ReservationController : ControllerBase
{
    private readonly ICommandBus commandBus;
    private readonly IQueryBus queryBus;

    public ReservationController(ICommandBus commandBus, IQueryBus queryBus)
    {
        this.commandBus = commandBus;
        this.queryBus = queryBus;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(CreateReservation command)
    {
        await this.commandBus.DispatchAsync(command);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<Reservation>> Get(Guid id)
    {
        var reservation = await this.queryBus.QueryAsync(new GetReservation(id));

        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }
}