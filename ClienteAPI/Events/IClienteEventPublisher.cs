using ClienteAPI.Models;

namespace ClienteAPI.Events;

public interface IClienteEventPublisher
{
    void PublicarClienteCreado(Cliente cliente);
    void PublicarClienteEliminado(Guid id);
    void PublicarClienteModificado(Cliente cliente);
}
