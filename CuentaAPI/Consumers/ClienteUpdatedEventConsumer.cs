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
    public ClienteUpdatedEventConsumer(IClienteRepository clienteRepository, ICuentaRepository cuentaRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _cuentaRepository = cuentaRepository;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<ClienteUpdatedEvent> context)
    {
        var clienteUpdated = context.Message;
        var result = await _clienteRepository.UpdateAsync(clienteUpdated.Id, _mapper.Map<Cliente>(clienteUpdated));
        if (!result) throw new NotFoundException($"El cliente {clienteUpdated.Id} no existe.");

        Console.WriteLine($"El cliente {clienteUpdated.Id} fue actualizado.");

        if (!clienteUpdated.Estado)
        {
            var numeroCuentas = await _cuentaRepository.DesactivarCuentasByClienteAsync(clienteUpdated.Id);
            Console.WriteLine($"{numeroCuentas} cuentas desactivadas del cliente {clienteUpdated.Id}.");
        }
    }
}
