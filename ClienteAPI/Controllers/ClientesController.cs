﻿using ClienteAPI.Contracts;
using ClienteAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClienteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;
    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _clienteService.ListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _clienteService.GetAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ClienteCreateRequest cliente)
    {
        var result = await _clienteService.CreateAsync(cliente);
        return CreatedAtAction(nameof(Get), result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] ClienteUpdateRequest clienteUpdateRequest)
    {
        var result = await _clienteService.UpdateAsync(id, clienteUpdateRequest);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _clienteService.DeleteAsync(id);
        return Ok(result);
    }
}
