using ClientApp.Services.Contracts;
using ClientApp.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClientGetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _clientService.ListAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ClientGetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _clientService.GetAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] ClientCreateRequest client)
    {
        var result = await _clientService.CreateAsync(client);
        return CreatedAtAction(nameof(Get), result);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Patch(Guid id, [FromBody] ClientUpdateRequest clientUpdateRequest)
    {
        var result = await _clientService.UpdateAsync(id, clientUpdateRequest);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _clientService.DeleteAsync(id);
        return Ok(result);
    }
}
