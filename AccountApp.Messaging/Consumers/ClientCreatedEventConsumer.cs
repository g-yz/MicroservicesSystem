using Shared.Messages;
using AutoMapper;
using MassTransit;
using AccountApp.Data.Models;
using AccountApp.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace AccountApp.Messaging.Consumers;

public class ClientCreatedEventConsumer : IConsumer<ClientCreatedEvent>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ClientCreatedEventConsumer> _logger;
    public ClientCreatedEventConsumer(IClientRepository clientRepository, IMapper mapper, ILogger<ClientCreatedEventConsumer> logger)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ClientCreatedEvent> context)
    {
        var ClienteCreated = context.Message;
        await _clientRepository.CreateAsync(_mapper.Map<Client>(ClienteCreated));

        _logger.LogInformation("The client {ClientId} was created.", ClienteCreated.Id);
    }
}
