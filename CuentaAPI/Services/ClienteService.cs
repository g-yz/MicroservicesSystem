using AppShared.Messages;
using MassTransit;

namespace CuentaAPI.Services;

public class ClienteService : IClienteService
{
    private readonly IBus _bus;
    public ClienteService(IBus bus)
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
