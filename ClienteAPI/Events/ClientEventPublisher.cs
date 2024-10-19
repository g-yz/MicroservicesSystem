using AppShared.Messages;
using MassTransit;

namespace ClienteAPI.Events;

public class ClientEventPublisher : IClientEventPublisher
{
    private readonly IBus _bus;

    public ClientEventPublisher(IBus bus)
    {
        _bus = bus;
    }

    public void NotificarClienteDesactivado(Guid clienteId)
    {
        var clientDeletedEvent = new ClienteDesactivadoEvent { ClienteId = clienteId };
        _bus.Publish(clientDeletedEvent);
        Console.WriteLine($"Cliente {clienteId} fue desactivado.");
    }
}
