using AppShared.Messages;
using ClienteAPI.Repositories;
using MassTransit;

namespace ClienteAPI.Consumers;

public class VerificarClienteCommandConsumer : IConsumer<VerificarClienteCommand>
{
    private readonly IClienteRepository _clienteRepository;
    public VerificarClienteCommandConsumer(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task Consume(ConsumeContext<VerificarClienteCommand> context)
    {
        var command = context.Message;
        var cliente = await _clienteRepository.GetAsync(command.ClienteId);
        await context.RespondAsync(new ClienteValidationResult{ Existe = cliente != null });
    }
}
