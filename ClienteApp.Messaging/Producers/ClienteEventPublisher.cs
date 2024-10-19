using SystemApp.Shared.Messages;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using ClienteApp.Data.Models;

namespace ClienteApp.Services.Producers;

public class ClienteEventPublisher : IClienteEventPublisher
{
    private readonly IBus _bus;
    private readonly IMapper _mapper;
    private readonly ILogger<ClienteEventPublisher> _logger;

    public ClienteEventPublisher(IBus bus, IMapper mapper, ILogger<ClienteEventPublisher> logger)
    {
        _bus = bus;
        _mapper = mapper;
        _logger = logger;
    }

    public void PublicarClienteCreado(Cliente cliente)
    {
        var clienteCreated = _mapper.Map<ClienteCreatedEvent>(cliente);
        _bus.Publish(clienteCreated);
        _logger.LogInformation("Cliente {ClienteId} fue creado.", clienteCreated.Id);
    }

    public void PublicarClienteEliminado(Guid id)
    {
        var clienteDeleted = new ClienteDeletedEvent() { Id = id };
        _bus.Publish(clienteDeleted);
        _logger.LogInformation("Cliente {ClienteId} fue eliminado.", clienteDeleted.Id);
    }

    public void PublicarClienteModificado(Cliente cliente)
    {
        var clienteUpdated = _mapper.Map<ClienteUpdatedEvent>(cliente);
        _bus.Publish(clienteUpdated);
        _logger.LogInformation("Cliente {ClienteId} fue creado.", clienteUpdated.Id);
    }
}
