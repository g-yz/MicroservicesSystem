using AppShared.Messages;
using AutoMapper;
using CuentaAPI.Models;
using CuentaAPI.Repositories;
using MassTransit;

namespace CuentaAPI.Consumers;

public class ClienteCreatedEventConsumer : IConsumer<ClienteCreatedEvent>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    public ClienteCreatedEventConsumer(IClienteRepository clienteRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<ClienteCreatedEvent> context)
    {
        var ClienteCreated = context.Message;
        await _clienteRepository.CreateAsync(_mapper.Map<Cliente>(ClienteCreated));

        Console.WriteLine($"El cliente {ClienteCreated.Id} fue creado.");
    }
}
