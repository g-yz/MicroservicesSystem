using Shared.Messages;
using AutoMapper;
using MassTransit;
using AccountApp.Data.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;
using AccountApp.Data.Models;

namespace AccountApp.Messaging.Consumers;

public class ClientUpdatedEventConsumer : IConsumer<ClientUpdatedEvent>
{
    private readonly IClientRepository _clientRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ClientUpdatedEventConsumer> _logger;
    public ClientUpdatedEventConsumer(IClientRepository clientRepository, IAccountRepository accountRepository, IMapper mapper, ILogger<ClientUpdatedEventConsumer> logger)
    {
        _clientRepository = clientRepository;
        _accountRepository = accountRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ClientUpdatedEvent> context)
    {
        var clientUpdated = context.Message;
        var result = await _clientRepository.UpdateAsync(clientUpdated.Id, _mapper.Map<Client>(clientUpdated));
        if (!result) throw new NotFoundException($"The client {clientUpdated.Id} does not exist.");

        _logger.LogInformation("The client {ClientId} was updated.", clientUpdated.Id);

        if (!clientUpdated.Status)
        {
            var accountNumbers = await _accountRepository.DeleteByClienteAsync(clientUpdated.Id);
            _logger.LogInformation("{AccountNumbers} accounts disabled of client {ClientId}.", accountNumbers, clientUpdated.Id);
        }
    }
}
