using SystemApp.Shared.Messages;
using CuentaAPI.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CuentaAPI.Consumers;

public class ClienteDeletedEventConsumer : IConsumer<ClienteDeletedEvent>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly ICuentaRepository _cuentaRepository;
    private readonly ILogger<ClienteDeletedEventConsumer> _logger;
    public ClienteDeletedEventConsumer(IClienteRepository clienteRepository, ICuentaRepository cuentaRepository, ILogger<ClienteDeletedEventConsumer> logger)
    {
        _clienteRepository = clienteRepository;
        _cuentaRepository = cuentaRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ClienteDeletedEvent> context)
    {
        var clienteId = context.Message.Id;
        var result = await _clienteRepository.DeleteAsync(clienteId);
        if (!result) throw new NotFoundException($"El cliente {clienteId} no existe.");

        _logger.LogInformation("Cliente {ClienteId} eliminado.", clienteId);

        var numeroCuentas = await _cuentaRepository.DeleteByClienteAsync(clienteId);
        _logger.LogInformation("{NumeroCuentas} cuentas desactivadas del cliente {ClienteId}.", numeroCuentas, clienteId);
    }
}
