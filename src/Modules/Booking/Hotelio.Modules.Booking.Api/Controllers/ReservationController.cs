using Microsoft.AspNetCore.Mvc;
using Hotelio.Shared.Commands;
using Hotelio.Shared.Queries;
using Hotelio.Modules.Booking.Application.ReadModel;
using Hotelio.Modules.Booking.Application.Query;
using Hotelio.Modules.Booking.Application.Command;

namespace Hotelio.Modules.Booking.Api.Controllers;

[ApiController]
[Route("api/reservation")]
internal class ReservationController : ControllerBase
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    public ReservationController(ICommandBus commandBus, IQueryBus queryBus)
    {
        _commandBus = commandBus;
        _queryBus = queryBus;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(CreateReservation command)
    {
        await this._commandBus.DispatchAsync(command);
        return NoContent();
    }

    [HttpPut("{id:guid}/pay/{key}")]
    public async Task<IActionResult> PayReservation(Guid id, string key, PayReservation command)
    {
        await _commandBus.DispatchAsync(command);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Reservation>> Get(Guid id)
    {
        var reservation = await _queryBus.QueryAsync(new GetReservation(id));

        if (reservation is null) {
            return NotFound();
        }

        return Ok(reservation);
    }
}