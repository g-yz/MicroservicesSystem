using AppShared.Messages;
using CuentaAPI.Services;
using MassTransit;

namespace CuentaAPI.Consumers;

public class ClienteDesactivadoEventConsumer : IConsumer<ClienteDesactivadoEvent>
{
    private readonly ICuentaService _cuentaService;
    public ClienteDesactivadoEventConsumer(ICuentaService cuentaService)
    {
        _cuentaService = cuentaService;
    }

    public async Task Consume(ConsumeContext<ClienteDesactivadoEvent> context)
    {
        var clienteId = context.Message.ClienteId;

        var numeroCuentas = await _cuentaService.DesactivarCuentasByClienteAsync(clienteId);

        Console.WriteLine($"{numeroCuentas} cuentas desactivadas del cliente {clienteId}.");
    }
}
