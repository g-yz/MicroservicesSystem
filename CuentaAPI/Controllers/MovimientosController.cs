using CuentaApp.Data.Models;
using CuentaApp.Services.Contracts;
using CuentaApp.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace CuentaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovimientosController : ControllerBase
{
    private readonly IMovimientoService _movimientoService;
    public MovimientosController(IMovimientoService movimientoService)
    {
        _movimientoService = movimientoService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] MovimientoAddRequest movimiento)
    {
         var result = await _movimientoService.CreateAsync(movimiento);
        return CreatedAtAction(nameof(GetReporte), result);
    }

    [HttpGet("reporte")]
    [ProducesResponseType(typeof(IEnumerable<ReporteMovimientosGetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReporte([FromQuery] MovimientoReporteFilter filters)
    {
        return Ok(await _movimientoService.GetReporteAsync(filters));
    }

    [HttpGet()]
    [ProducesResponseType(typeof(IEnumerable<MovimientoGetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _movimientoService.ListAsync());
    }
}
