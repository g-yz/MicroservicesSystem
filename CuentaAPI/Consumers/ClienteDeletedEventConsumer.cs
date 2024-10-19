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
        if (!result) throw new NotFoundException($"El cliente {clienteId} no existe.");

        Console.WriteLine($"Cliente {clienteId} eliminado.");

        var numeroCuentas = await _cuentaRepository.DesactivarCuentasByClienteAsync(clienteId);
        Console.WriteLine($"{numeroCuentas} cuentas desactivadas del cliente {clienteId}.");
    }
}
