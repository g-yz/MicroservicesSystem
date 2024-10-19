using AppShared.Messages;
using AutoMapper;
using ClienteAPI.Models;
using MassTransit;

namespace ClienteAPI.Events;

public class ClienteEventPublisher : IClienteEventPublisher
{
    private readonly IBus _bus;
    private readonly IMapper _mapper;

    public ClienteEventPublisher(IBus bus, IMapper mapper)
    {
        _bus = bus;
        _mapper = mapper;
    }

    public void PublicarClienteCreado(Cliente cliente)
    {
        var clienteCreated = _mapper.Map<ClienteCreatedEvent>(cliente);
        _bus.Publish(clienteCreated);
        Console.WriteLine($"Cliente {clienteCreated.Id} fue creado.");
    }

    public void PublicarClienteEliminado(Guid id)
    {
        var clienteDeleted = new ClienteDeletedEvent() { Id = id };
        _bus.Publish(clienteDeleted);
        Console.WriteLine($"Cliente {clienteDeleted.Id} fue eliminado.");
    }

    public void PublicarClienteModificado(Cliente cliente)
    {
        var clienteUpdated = _mapper.Map<ClienteUpdatedEvent>(cliente);
        _bus.Publish(clienteUpdated);
        Console.WriteLine($"Cliente {clienteUpdated.Id} fue creado.");
    }
}
