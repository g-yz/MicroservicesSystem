namespace ClienteAPI.Events;

public interface IClienteEventPublisher
{
    void NotificarClienteDesactivado(Guid clienteId);
}
