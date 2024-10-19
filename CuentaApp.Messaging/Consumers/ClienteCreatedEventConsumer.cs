using SystemApp.Shared.Messages;
using AutoMapper;
using MassTransit;
using CuentaApp.Data.Models;
using CuentaApp.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace CuentaApp.Messaging.Consumers;

public class ClienteCreatedEventConsumer : IConsumer<ClienteCreatedEvent>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ClienteCreatedEventConsumer> _logger;
    public ClienteCreatedEventConsumer(IClienteRepository clienteRepository, IMapper mapper, ILogger<ClienteCreatedEventConsumer> logger)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ClienteCreatedEvent> context)
    {
        var ClienteCreated = context.Message;
        await _clienteRepository.CreateAsync(_mapper.Map<Cliente>(ClienteCreated));

        _logger.LogInformation("El cliente {ClientId} fue creado.", ClienteCreated.Id);
    }
}
