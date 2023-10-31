using Microsoft.AspNetCore.Mvc;
using Hotelio.Shared.Commands;
using Hotelio.Shared.Queries;
using Hotelio.Modules.Booking.Application.ReadModel;
using Hotelio.Modules.Booking.Application.Query;
using Hotelio.Modules.Booking.Application.Command;
using MediatR;

namespace Hotelio.Modules.Booking.Api.Controllers;

[ApiController]
[Route("api/reservation")]
internal class ReservationController : ControllerBase
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;
    private readonly IMediator _mediator;

    public ReservationController(ICommandBus commandBus, IQueryBus queryBus, IMediator _mediator)
    {
        this._commandBus = commandBus;
        this._queryBus = queryBus;
        this._mediator = _mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(CreateReservation command)
    {
        await this._commandBus.DispatchAsync(command);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Reservation>> Get(Guid id)
    {
        var reservation = await this._queryBus.QueryAsync(new GetReservation(id));

        if (reservation is null) {
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