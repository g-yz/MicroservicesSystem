using Shared.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using AccountApp.Data.Repositories;
using Shared.Exceptions;

namespace AccountApp.Messaging.Consumers;

public class ClientDeletedEventConsumer : IConsumer<ClientDeletedEvent>
{
    private readonly IClientRepository _clientRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<ClientDeletedEventConsumer> _logger;
    public ClientDeletedEventConsumer(IClientRepository clientRepository, IAccountRepository accountRepository, ILogger<ClientDeletedEventConsumer> logger)
    {
        _clientRepository = clientRepository;
        _accountRepository = accountRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ClientDeletedEvent> context)
    {
        var clientId = context.Message.Id;
        var result = await _clientRepository.DeleteAsync(clientId);
        if (!result) throw new NotFoundException($"The client {clientId} does not exist.");

        _logger.LogInformation("Client {ClientId} removed.", clientId);

        var accountNumbers = await _accountRepository.DeleteByClienteAsync(clientId);
        _logger.LogInformation("{AccountNumbers} accounts disabled of client {ClientId}.", accountNumbers, clientId);
    }
}
