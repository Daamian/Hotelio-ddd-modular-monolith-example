﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using Hotelio.Shared.Commands;
using Hotelio.Shared.Queries;
using Hotelio.Modules.Booking.Application.ReadModel;
using Hotelio.Modules.Booking.Application.Query;
using Hotelio.Modules.Booking.Application.Command;
using Microsoft.AspNetCore.Authorization;

namespace Hotelio.Modules.Booking.Api.Controllers;

[ApiController]
[Route("api/reservation")]
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Reservation>> Get(Guid id)
    {
        var reservation = await this.queryBus.QueryAsync(new GetReservation(id));

        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok("Hello reservation");
    }
}