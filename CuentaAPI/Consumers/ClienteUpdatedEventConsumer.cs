using AppShared.Messages;
using AutoMapper;
using CuentaAPI.Models;
using CuentaAPI.Repositories;
using MassTransit;

namespace CuentaAPI.Consumers;

public class ClienteUpdatedEventConsumer : IConsumer<ClienteUpdatedEvent>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly ICuentaRepository _cuentaRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ClienteUpdatedEventConsumer> _logger;
    public ClienteUpdatedEventConsumer(IClienteRepository clienteRepository, ICuentaRepository cuentaRepository, IMapper mapper, ILogger<ClienteUpdatedEventConsumer> logger)
    {
        _clienteRepository = clienteRepository;
        _cuentaRepository = cuentaRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ClienteUpdatedEvent> context)
    {
        var clienteUpdated = context.Message;
        var result = await _clienteRepository.UpdateAsync(clienteUpdated.Id, _mapper.Map<Cliente>(clienteUpdated));
        if (!result) throw new NotFoundException($"El cliente {clienteUpdated.Id} no existe.");

        _logger.LogInformation("El cliente {ClienteId} fue actualizado.", clienteUpdated.Id);

        if (!clienteUpdated.Estado)
        {
            var numeroCuentas = await _cuentaRepository.DeleteByClienteAsync(clienteUpdated.Id);
            _logger.LogInformation("{NumeroCuentas} cuentas desactivadas del cliente {ClienteId}.", numeroCuentas, clienteUpdated.Id);
        }
    }
}
