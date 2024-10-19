﻿using ClienteApp.Data.Models;

namespace ClienteApp.Messaging.Producers;

public interface IClienteEventPublisher
{
    void PublicarClienteCreado(Cliente cliente);
    void PublicarClienteEliminado(Guid id);
    void PublicarClienteModificado(Cliente cliente);
}
