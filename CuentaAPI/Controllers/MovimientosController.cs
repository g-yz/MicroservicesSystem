using CuentaAPI.Contracts;
using CuentaAPI.Services;
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
    public async Task<IActionResult> Post([FromBody] MovimientoAddRequest movimiento)
    {
         var result = await _movimientoService.CreateAsync(movimiento);
        return CreatedAtAction(nameof(GetReporte), result);
    }

    [HttpGet("reporte")]
    public async Task<IActionResult> GetReporte([FromQuery] MovimientoReporteFilter filters)
    {
        return Ok(await _movimientoService.GetReporteAsync(filters));
    }
}
