using CuentaApp.Services.Contracts;
using CuentaApp.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace CuentaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CuentasController : ControllerBase
{
    private readonly ICuentaService _cuentaService;
    public CuentasController(ICuentaService cuentaService)
    {
        _cuentaService = cuentaService;
    }
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CuentaGetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _cuentaService.ListAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CuentaGetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _cuentaService.GetAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] CuentaCreateRequest cuentaCreateRequest)
    {
        var result = await _cuentaService.CreateAsync(cuentaCreateRequest);
        return CreatedAtAction(nameof(Get), result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put(Guid id, [FromBody] CuentaUpdateRequest cuentaUpdateRequest)
    {
        var result =  await _cuentaService.UpdateAsync(id, cuentaUpdateRequest);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _cuentaService.DeleteAsync(id);
        return Ok(result);
    }
}
