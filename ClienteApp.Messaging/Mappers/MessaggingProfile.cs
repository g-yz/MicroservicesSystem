using SystemApp.Shared.Messages;
using AutoMapper;
using ClienteApp.Data.Models;

namespace ClienteApp.Messaging.Mappers;

public class MessaggingProfile : Profile
{
    public MessaggingProfile()
    {
        CreateMap<Cliente, ClienteCreatedEvent>();
        CreateMap<Cliente, ClienteUpdatedEvent>();
    }
}
