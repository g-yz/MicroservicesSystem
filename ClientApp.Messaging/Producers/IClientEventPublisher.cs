using ClientApp.Data.Models;

namespace ClientApp.Messaging.Producers;

public interface IClientEventPublisher
{
    void PublicarClienteCreado(Client client);
    void PublicarClienteEliminado(Guid id);
    void PublicarClienteModificado(Client client);
}
