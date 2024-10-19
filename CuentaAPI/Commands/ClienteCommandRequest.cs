using AppShared.Messages;
using MassTransit;

namespace CuentaAPI.Commands;

public class ClienteCommandRequest : IClienteCommandRequest
{
    private readonly IBus _bus;
    public ClienteCommandRequest(IBus bus)
    {
        _bus = bus;
    }
    public async Task<bool> VerificarClienteAsync(Guid id)
    {
        var command = new VerificarClienteCommand { ClienteId = id };

        var response = await _bus.Request<VerificarClienteCommand, ClienteValidationResult>(command);

        return response.Message.Existe;
    }
}
