using Shared.Messages;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using ClientApp.Data.Models;

namespace ClientApp.Messaging.Producers;

public class ClientEventPublisher : IClientEventPublisher
{
    private readonly IBus _bus;
    private readonly IMapper _mapper;
    private readonly ILogger<ClientEventPublisher> _logger;

    public ClientEventPublisher(IBus bus, IMapper mapper, ILogger<ClientEventPublisher> logger)
    {
        _bus = bus;
        _mapper = mapper;
        _logger = logger;
    }

    public void PublicarClienteCreado(Client client)
    {
        var clientCreated = _mapper.Map<ClientCreatedEvent>(client);
        _bus.Publish(clientCreated);
        _logger.LogInformation("Client {ClientId} was created.", clientCreated.Id);
    }

    public void PublicarClienteEliminado(Guid id)
    {
        var clientDeleted = new ClientDeletedEvent() { Id = id };
        _bus.Publish(clientDeleted);
        _logger.LogInformation("Client {ClientId} was removed.", clientDeleted.Id);
    }

    public void PublicarClienteModificado(Client client)
    {
        var clientUpdated = _mapper.Map<ClientUpdatedEvent>(client);
        _bus.Publish(clientUpdated);
        _logger.LogInformation("Client {ClientId} was updated.", clientUpdated.Id);
    }
}
