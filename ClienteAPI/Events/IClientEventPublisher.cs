namespace ClienteAPI.Events;

public interface IClientEventPublisher
{
    void NotificarClienteDesactivado(Guid clienteId);
}
