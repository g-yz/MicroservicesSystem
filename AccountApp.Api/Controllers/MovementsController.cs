using AccountApp.Data.Models;
using AccountApp.Services.Contracts;
using AccountApp.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovementsController : ControllerBase
{
    private readonly IMovementService _movementService;
    public MovementsController(IMovementService movementService)
    {
        _movementService = movementService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] MovementAddRequest movement)
    {
         var result = await _movementService.CreateAsync(movement);
        return CreatedAtAction(nameof(GetReporte), result);
    }

    [HttpGet("reporte")]
    [ProducesResponseType(typeof(IEnumerable<ReportOfMovementsGetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReporte([FromQuery] MovementReporteFilter filters)
    {
        return Ok(await _movementService.GetReporteAsync(filters));
    }

    [HttpGet()]
    [ProducesResponseType(typeof(IEnumerable<MovementGetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _movementService.ListAsync());
    }
}
