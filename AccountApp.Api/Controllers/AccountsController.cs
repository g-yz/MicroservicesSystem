using AccountApp.Services.Contracts;
using AccountApp.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AccountGetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _accountService.ListAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AccountGetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _accountService.GetAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] AccountCreateRequest accountCreateRequest)
    {
        var result = await _accountService.CreateAsync(accountCreateRequest);
        return CreatedAtAction(nameof(Get), result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put(Guid id, [FromBody] AccountUpdateRequest accountUpdateRequest)
    {
        var result =  await _accountService.UpdateAsync(id, accountUpdateRequest);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _accountService.DeleteAsync(id);
        return Ok(result);
    }
}
