using AppShared.Messages;
using CuentaAPI.Repositories;
using MassTransit;

namespace CuentaAPI.Consumers;

public class ClienteDeletedEventConsumer : IConsumer<ClienteDeletedEvent>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly ICuentaRepository _cuentaRepository;
    public ClienteDeletedEventConsumer(IClienteRepository clienteRepository, ICuentaRepository cuentaRepository)
    {
        _clienteRepository = clienteRepository;
        _cuentaRepository = cuentaRepository;
    }

    public async Task Consume(ConsumeContext<ClienteDeletedEvent> context)
    {
        var clienteId = context.Message.Id;
        var result = await _clienteRepository.DeleteAsync(clienteId);
        Console.WriteLine($"Cliente {clienteId} eliminado.");

        if (result)
        {
            var numeroCuentas = await _cuentaRepository.DesactivarCuentasByClienteAsync(clienteId);
            Console.WriteLine($"{numeroCuentas} cuentas desactivadas del cliente {clienteId}.");
        }
    }
}
